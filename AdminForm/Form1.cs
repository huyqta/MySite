﻿using AdminForm.Helpers;
using AdminForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminForm
{
    public partial class Form1 : Form
    {
        MySiteContent context = new MySiteContent();

        public Form1()
        {
            InitializeComponent();
            LoadListBooks();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Book book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = txtTitle.Text,
                Publisher = txtPublisher.Text,
                Author = txtAuthor.Text,
                UrlDownload = txtLinkDownload.Text,
                UrlImage = txtLinkImage.Text,
                UrlRead = txtReadOnline.Text,
                UrlThumbnail = txtLinkThumbnail.Text,
                Category = cboCategory.ValueMember
            };
            context.Books.Add(book);
            context.SaveChanges();
            LoadListBooks();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadListBooks()
        {
            DataTable dtbBooks = new DataTable();
            
            //dtbBooks.Columns.Add(new DataColumn() { Caption = "Title", ColumnName = "Title" });
            //dtbBooks.Columns.Add(new DataColumn() { Caption = "Author", ColumnName = "Author" });
            //dtbBooks.Columns.Add(new DataColumn() { Caption = "Publisher", ColumnName = "Publisher" });
            //dtbBooks.Columns.Add(new DataColumn() { Caption = "Description", ColumnName = "Description" });
            
            //dtbBooks = context.Books.ToList().ToDataTable();
            gridEbooks.DataSource = context.Books.Select(o=> new {
                Title = o.Title,
                Author = o.Author,
                Publisher = o.Publisher,
                Description = o.Description }).ToList();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            GoogleDriverConsole gdc = new GoogleDriverConsole();
            gdc.UploadFile();
            txtLinkDownload.Text = gdc.FileId;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            GoogleDriverConsole gdc = new GoogleDriverConsole();
            gdc.DownloadFile(txtLinkDownload.Text);
        }
    }
}