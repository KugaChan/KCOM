using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KCOM
{
#if false
    public class eNode<T>
    {
        public T data;          //数据域,当前结点数据

        public eNode<T> next;   //位置域,下一个结点地址

        public eNode(T item)
        {
            this.data = item;
            this.next = null;
        }

        public eNode()
        {
            this.data = default(T);
            this.next = null;
        }
    }

    class eLink<T>
    {
        private readonly object elink_lock = new object();

        public eNode<T> next_node;      //单链表头
        public eNode<T> last_node;      //单链表尾
        public int nr_entry;            //元素个数
        
        public eLink()      //构造
        {
            Clear();
        }
        
        /// <summary>
        /// 判断单键表是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if(nr_entry == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 清空单链表
        /// </summary>
        public void Clear()
        {
            next_node = null;
            last_node = null;
            nr_entry = 0;
        }

        //新增一个元素到队伍尾部
        public void PushTail(eNode<T> new_node)
        {
            lock(elink_lock)    //lock里面return也是会解锁的
            {
                nr_entry++;

                if(next_node == null)
                {
                    next_node = new_node;
                    last_node = new_node;
                    return;
                }

                if(last_node != null)
                {
                    last_node.next = new_node;
                    last_node = new_node;

                    return;
                }
            }

            Dbg.Assert(false, "###eLink is corrupt!");
        }
        
        //弹出第一个元素
        public eNode<T> PopTop()
        {
            if(IsEmpty())
            {
                Console.WriteLine("###eLink is empty");
                return null;
            }

            lock(elink_lock)    //lock里面return也是会解锁的
            {
                nr_entry--;

                eNode<T> A = next_node;

                next_node = next_node.next;
                if(next_node == null)                                           //源sgl一个都没有了，则把last也清掉
                {
                    last_node = null;
                }

                return A;
            }
        }
    }
#endif

    public class PNode<Y>
    {
        public Y obj;
        public int index;
        public bool taken;
    }

    public class ePool<T>
    {
        private readonly object epool_lock = new object();

        public int nr_ent;  //元素个数
        public int nr_got;  //目前已经取走了多少个元素了

        private int[] dbg_op_log = new int[1024];
        private int[] dbg_got_log = new int[1024];
        private int dbg_cnt;
        private void DbgAddLog(int op, int num)
        {
            dbg_op_log[dbg_cnt] = op;
            dbg_got_log[dbg_cnt] = num;
            dbg_cnt++;
            if(dbg_cnt >= 1024)
            {
                dbg_cnt = 0;
            }
        }
        private void DbgDumpLog()
        {
            Dbg.WriteLine("Final cnt:%", dbg_cnt);
            for(int i = 0; i < 1024; i++)
            {
                Dbg.WriteLine("i:% op:% got:%", i, dbg_op_log[i], dbg_got_log[i]);
            }
        }

        private List<PNode<T>> node_buffer = new List<PNode<T>>();

#if false
        public int TotalNum()
        {
            lock (epool_lock)
            {
                return nr_ent;
            }
        }

        public int GotNum()
        {
            lock (epool_lock)
            {
                return nr_got;
            }
        }
#endif

        public void Add(PNode<T> x, T obj)
        {
            x.obj = obj;
            x.index = nr_ent;
            x.taken = false;

            nr_ent++;

            node_buffer.Add(x);
        }

        public PNode<T> Get()
        {
            PNode<T> res = null;

            lock(epool_lock)
            {
                int i;
                for(i = 0; i < nr_ent; i++)
                {
                    PNode<T> p = node_buffer[i];

                    if(p.taken == false)
                    {
                        p.taken = true;
                        nr_got++;
                        DbgAddLog(1, nr_got);
                        Dbg.Assert(nr_got <= nr_ent, "###pool get error");
                        res = p;
                        break;
                    }
                }
            }

            return res;
        }

        public void Put(PNode<T> free_node)
        {
            lock(epool_lock)
            {
                PNode<T> p = node_buffer[free_node.index];
                Dbg.Assert(p.taken == true, "###pool put error1");
                p.taken = false;
                nr_got--;

                DbgAddLog(-1, nr_got);
                if(nr_got < 0)
                {
                    DbgDumpLog();
                }
                Dbg.Assert(nr_got >= 0, "###pool put error2");
            }
        }
    }

    //将object按照FIFO的方式搬移，本身没有数据本体
    public class eFIFO<T>
    {
        private readonly object efifo_lock = new object();

        public int top;
        public int bottom;
        public bool is_full;

        public int max_number;
        List<T> buffer;

        public eFIFO()
        {
            max_number = 0;
            buffer = new List<T>();
            Reset();
        }

        public void Reset()
        {
            top = 0;
            bottom = 0;
            is_full = false;
        }

        //决定了这个equeue的队列深度有多长，最多能缓存住多少个node
        public void Init(int _max_number)
        {
            max_number = _max_number;

            for(int i = 0; i < max_number; i++)
            {
                T x = default(T);

                buffer.Add(x);
            }

            Reset();
        }

        public int GetValidNum()
        {
            int num;

            lock(efifo_lock)
            {
                if(is_full == true)
                {
                    num = max_number;
                }
                else
                {
                    if(top < bottom)
                    {
                        num = top + max_number - bottom;
                    }
                    else
                    {
                        num = top - bottom;
                    }
                }
            }

            return num;
        }

        public T Output()
        {
            T data;

            lock(efifo_lock)
            {
                data = buffer[bottom];

#if SUPPORT_SHOW_FIFO_DATA
                Console.WriteLine("out:{0}({1}:{2})", value, top, bottom);
                for(int i = 0; i < buffer_value[bottom]; i++)
                {
                    Console.Write(" {0}", buffer_data[bottom][i]);
                }
                Console.WriteLine("({0}:{1})", top, bottom);
#endif

                is_full = false;
                bottom++;
                if(bottom >= max_number)
                {
                    bottom = 0;
                }
            }

            return data;
        }

        public void Input(T data)
        {
            lock(efifo_lock)
            {
                buffer[top] = data;

#if SUPPORT_SHOW_FIFO_DATA
                Console.WriteLine("in:{0}({1}:{2})", value, top, bottom);
                for(int i = 0; i < buffer_value[top]; i++)
                {
                    Console.Write(" {0}", buffer_data[top][i]);
                }
                Console.WriteLine("({0}:{1})", top, bottom);
#endif
                top++;

                if(top >= max_number)
                {
                    top = 0;
                }
                if(top == bottom)   //如果头部赶上尾部，则FIFO已满
                {
                    is_full = true;
                }
            }
        }
    }
}
