using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab6.DB;
using Microsoft.Reporting.WinForms;

namespace Lab6
{
    public partial class reports : Form
    {
        Model1 db = new Model1();   
        public reports()
        {
            InitializeComponent();
        }

        private void reports_Load(object sender, EventArgs e)
        {


            reportViewer1.LocalReport.ReportPath = "C:\\Users\\AkayS\\source\\repos\\Lab6\\Lab6\\Report1.rdlc";
            var query = from b in db.Sach
                        join c in db.LoaiSach on b.MaLoai equals c.MaLoai
                        orderby b.NamXB descending
                        select new
                        {
                            NamXB = b.NamXB,
                            MaSach = b.MaSach,
                            TenSach = b.TenSach,
                            TenTheLoai = c.TenLoai
                        };
            DataTable dt = new DataTable();
            dt.Columns.Add("NamXB", typeof(int));
            dt.Columns.Add("MaSach", typeof(string));
            dt.Columns.Add("TenSach", typeof(string));
            dt.Columns.Add("MaLoai", typeof(string));

            foreach (var item in query)
            {
                dt.Rows.Add(item.NamXB, item.MaSach, item.TenSach, item.TenTheLoai);
                Console.WriteLine($"Số lượng bản ghi: {query.Count()}");
            }
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1"; // Tên dataset trong file RDLC
            rds.Value = dt;

            // Xóa các nguồn dữ liệu cũ và thêm nguồn mới
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}

        
