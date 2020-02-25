using Dapper;
using MOT.domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MOT.view.manager
{
    /// <summary>
    /// ProductPage.xaml 的交互逻辑
    /// </summary>
    public partial class ProductPage : Page
    {
        private String productId;

        private ObservableCollection<ProductItem> productItems;

        private User user = AccountHelper.Account.Instance.GetUser();

        public ProductPage(string productId)
        {
            InitializeComponent();
            this.productId = productId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            labelAuthNum.Content = user.user_name + "，您好！您的领用权限是：" + user.NumAuth();

            string sqlServer = ConfigurationManager.AppSettings["MySQLUrl"];
            // String productQuery = "select *  FROM product WHERE product.pid = @pid;";

            string query = "SELECT product_item.plid, product_item.pid, product_item.mid, product_item.num, product_item.maxsafe_repo,product_item.minwarning_repo,product_item.pred_knife_num,material.rest,material.get_max " +
                "FROM product_item, material WHERE product_item.pid = @pid AND material.mid = product_item.mid;";
            using (IDbConnection connection = new MySqlConnection(sqlServer))
            {
                List<ProductItem> items = connection.Query<ProductItem>(query, new { pid = productId }).ToList();

                //   var product = connection.Query<Product>(productQuery, new { pid = productId });
                foreach (ProductItem item in items)
                {
                    // 如果所需刀具个数多于库存，以库存为主
                    item.Num = item.Num > item.rest ? item.rest : item.Num;
                    item.Get_num = item.get_max > item.Num ? item.Num : item.get_max;
                }
                productItems = new ObservableCollection<ProductItem>(items);
                lvMaterials.ItemsSource = productItems;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // 点击返回
            this.NavigationService.GoBack();
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            // 将选择数量为0的，从数据中删除。倒叙保证下标位置。
            // 如果选择的全是零，那么应该有提示
            int count = productItems.Count();
            foreach (ProductItem item in productItems)
            {
                if (item.Get_num == 0)
                {
                    count--;
                }
            }

            if (count == 0)
            {
                MessageBox.Show("请至少选择一种刀具，且数量不为零");
                return;
            }
            ConfirmPage confirmPage = new ConfirmPage(productItems.AsList());
            this.NavigationService.Navigate(confirmPage);
        }


        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            lvMaterials.SelectedItem = ((Button)sender).DataContext;
            ProductItem item = lvMaterials.SelectedItem as ProductItem;

            if (item.Get_num > 0)
            {
                item.Get_num--;
                if (item.Get_num <= (item.rest - 1) && item.Get_num <= (user.NumAuth() - 1) && item.Get_num <= (item.Num - 1) && item.Get_num <= (item.get_max))
                {
                    item.UpAvailable = true;

                }
                if (item.Get_num == 0)
                {
                    item.DownAvailable = false;
                }
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("确定不需要此种刀具吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.OK)
            {
                lvMaterials.SelectedItem = ((Button)sender).DataContext;
                ProductItem item = lvMaterials.SelectedItem as ProductItem;

                productItems.Remove(item);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            lvMaterials.SelectedItem = ((Button)sender).DataContext;
            ProductItem item = lvMaterials.SelectedItem as ProductItem;
            if (item.Get_num < item.rest && item.Get_num < user.NumAuth() && item.Get_num < item.Num && item.Get_num < item.get_max)
            {
                item.Get_num++;
                if (item.Get_num == 1)
                {
                    item.DownAvailable = true;

                }
                if (item.Get_num == item.rest && item.Get_num == user.NumAuth() && item.Get_num == item.Num && item.Get_num == item.get_max)
                {
                    item.UpAvailable = false;
                }
            }
            else
            {
                MessageBox.Show("数量不可超过库存或权限！");
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox ItemNum = (TextBox)sender;
                lvMaterials.SelectedItem = ((TextBox)sender).DataContext;
                ProductItem item = lvMaterials.SelectedItem as ProductItem;
                string strNum = ItemNum.Text as string;
                if ("" == strNum || null == strNum)
                {
                    item.Get_num = 0;
                    item.DownAvailable = false;
                    item.UpAvailable = true;
                }
                else
                {
                    int num = int.Parse(strNum);
                    if (0 < num && num < item.rest && num < user.NumAuth() && num < item.Num && num < item.get_max)
                    {
                        item.Get_num = num;
                        item.DownAvailable = true;
                        item.UpAvailable = true;
                    }
                    else
                    {
                        if (num == Math.Min(user.NumAuth(), Math.Min(item.get_max, Math.Min(item.rest, item.Num))))
                        {
                            item.Get_num = num;
                            item.UpAvailable = false;
                            item.DownAvailable = true;
                        }
                        else if (num < 0)
                        {
                            MessageBox.Show("刀具数量不能为负");
                            ((TextBox)sender).Text = Convert.ToString(item.Num);
                        }
                        else if (num > item.get_max)
                        {
                            MessageBox.Show("刀具数量不能超过额定领用上限");
                            ((TextBox)sender).Text = Convert.ToString(item.Num);
                        }
                        else if (num > item.Num)
                        {
                            MessageBox.Show("刀具数量不能超过产品刀片数量");
                            ((TextBox)sender).Text = Convert.ToString(item.Num);
                        }
                        else if (num > item.rest || num > user.NumAuth())
                        {
                            MessageBox.Show("数量不可超过库存或权限！");
                            ((TextBox)sender).Text = Convert.ToString(item.Num);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // 过滤按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9)))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
