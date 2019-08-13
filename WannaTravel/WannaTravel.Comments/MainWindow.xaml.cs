using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using WannaTravel.BusinessLogic.Entities;

namespace WannaTravel.Comments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            List<CommentEntity> commments = null;
            InitializeComponent();

            Task.Run(() =>
            {
                while(true)
                { 
                    using (var handler = new HttpClientHandler())
                    using (var client = new HttpClient(handler))
                    {
                        var response = client.GetAsync(@"http://localhost:53324/api/Comment/").ContinueWith((body) =>
                        {

                            var bodyStream = body.Result;
                            var jsonString = bodyStream.Content.ReadAsStringAsync();
                            commments = JsonConvert.DeserializeObject<List<CommentEntity>>(jsonString.Result);
                        });

                        response.Wait();

                        var disp = Application.Current.Dispatcher;
                        disp.Invoke(() =>
                        {
                            Debug.WriteLine(commments.Count);

                            // todo: add id to each comment0
                            lvDataBinding.ItemsSource = commments;
                        });
                    }
                }
            });
        }
    }
}
