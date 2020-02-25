using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOT.domain
{
    public class ProductItem : INotifyPropertyChanged
    {
        private User user = AccountHelper.Account.Instance.GetUser();

        // 自己id
        public string plid { get; set; }

        // 所属ProductId
        public string pid { get; set; }

        // 刀具id
        public string mid { get; set; }

        // 一个产品需要此种刀具的数量
        private int num;

        private Boolean available;

        public int Num
        {
            get
            {
                return this.num;
            }

            set
            {
                if (this.num != value)
                {
                    if (user.NumAuth() > 0 && user.type == Constant.USER_TYPE_ADMIN)
                    {
                        this.num = value > user.NumAuth()?user.NumAuth():value;
                    }
                    else
                    {
                        this.num = value;
                    }
                    
                    this.NotifyPropertyChanged("Num");
                }
            }
        }

        public int get_max { get; set; }


        private int get_num;

        public int Get_num
        {
            get { return this.get_num; }
            set
            {
                if (this.get_num != value)
                {
                    if (user.NumAuth() > 0 && user.type == Constant.USER_TYPE_ADMIN)
                    {
                        this.get_num = (value > user.NumAuth() ? user.NumAuth() : value);
                    }
                    else
                    {
                        this.get_num = value;
                    }
                    //this.get_num = value;
                    this.NotifyPropertyChanged("Get_num");
                }
            }
        }


        private Boolean upAvailable;

        public Boolean UpAvailable
        {
            get
            {
                if (user.type==Constant.USER_TYPE_ADMIN)
                {
                    return (this.get_num < this.rest) && (this.get_num < user.NumAuth()&&(this.get_num<this.num)&&(this.get_num<this.get_max));
                }
                return (this.get_num<this.rest)&&(this.get_max<this.num)&&(this.get_num<this.get_max);
            }
            set
            {
                upAvailable = value;
                NotifyPropertyChanged("UpAvailable");
            }
        }

        private Boolean downAvailable;
        public Boolean DownAvailable
        {
            get { return this.get_num >0; }
            set { downAvailable = value; NotifyPropertyChanged("DownAvailable"); }

        }

        // 最大安全库存
        public int maxsafe_repo { get; set; }

        // 最小警戒库存
        public int minwarning_repo { get; set; }

        // 预测刀片数量
        public int pred_knife_num { get; set; }

        public int rest { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
