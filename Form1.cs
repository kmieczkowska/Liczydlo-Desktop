using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace liczydlo
{
    public partial class Form1 : Form
    {
        string filePath1 = "", filePath2 = "";

        public Form1()
        {
            InitializeComponent();
            openCatalogButton.Visible = false;
            openNewFileButton.Visible = false;
            pictureBox1.Image = Image.FromFile("C:\\Users\\klaud\\Downloads\\cat.png");
            toolTip1.SetToolTip(button1, "To bêdzie plik bazowy");
            toolTip1.SetToolTip(button2, "Ten plik do³¹czysz do bazowego - jeœli ma nowe kolumny, to bêd¹ na koñcu");
            toolTip1.SetToolTip(button3, "Scalenie dwóch plików, zada pytanie gdzie zapisaæ scalony plik");
            toolTip1.SetToolTip(button2, "Otwiera folder z nowym plikiem");
            toolTip1.SetToolTip(button2, "Otwiera nowy plik w aplikacji Excel");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath1 = ofd.FileName;
                excel1.Text = filePath1;
                excel1.SelectionStart = 0;
                excel1.ScrollToCaret();
                //excel1.Text = Path.GetFileName(filePath1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel Files|*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath2 = ofd.FileName;
                excel2.Text = filePath2;
                excel2.SelectionStart = 0;
                excel2.ScrollToCaret();
                //excel2.Text = Path.GetFileName(filePath2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openCatalogButton.Visible = true;
            openNewFileButton.Visible = true;

            if (filePath1 == "" || filePath2 == "")
            {
                MessageBox.Show("Wybierz dwa pliki zanim scalisz!");
                return;
            }

            try
            {
                var data1 = LoadExcelToDictionary(filePath1);
                var data2 = LoadExcelToDictionary(filePath2);

                // scalona lista wszystkich kolumn
                var allColumns = new HashSet<string>(data1.Values.SelectMany(d => d.Keys));
                allColumns.UnionWith(data2.Values.SelectMany(d => d.Keys));

                // scalona lista osób
                var allPeople = new HashSet<string>(data1.Keys);
                allPeople.UnionWith(data2.Keys);

                // przygotuj DataTable
                DataTable mergedTable = new DataTable();
                mergedTable.Columns.Add("Imiê + nazwisko");

                foreach (var col in allColumns)
                    mergedTable.Columns.Add(col);

                foreach (var person in allPeople)
                {
                    var row = mergedTable.NewRow();
                    row["Imiê + nazwisko"] = person;

                    if (data1.ContainsKey(person))
                        foreach (var kvp in data1[person])
                            row[kvp.Key] = kvp.Value;

                    if (data2.ContainsKey(person))
                        foreach (var kvp in data2[person])
                            row[kvp.Key] = kvp.Value;

                    mergedTable.Rows.Add(row);
                }

                // zapisz do Excela
                SaveFileDialog sfd = new SaveFileDialog();
                var longFilePath = sfd.FileName;
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.FileName = "Scalony.xlsx";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Wynik");
                        ws.Cell(1, 1).InsertTable(mergedTable);
                        wb.SaveAs(sfd.FileName);
                    }
                    pathLabel.ForeColor = Color.DeepPink;
                    pathLabel.Text = sfd.FileName;
                    MessageBox.Show("Saclanie plików powiod³o siê", "Dziunia, to dzia³a");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d: " + ex.Message);
            }
        }

        // funkcja pomocnicza - ³adowanie Excela do s³ownika
        private Dictionary<string, Dictionary<string, string>> LoadExcelToDictionary(string path)
        {
            var dict = new Dictionary<string, Dictionary<string, string>>();

            using (var wb = new XLWorkbook(path))
            {
                var ws = wb.Worksheet(1);
                var rows = ws.RangeUsed().RowsUsed().ToList();
                var headers = rows[0].Cells().Select(c => c.Value.ToString()).ToList();

                for (int i = 1; i < rows.Count; i++)
                {
                    var cells = rows[i].Cells().Select(c => c.Value.ToString()).ToList();
                    if (cells.Count == 0) continue;

                    string key = cells[0]; // Imiê + nazwisko
                    var innerDict = new Dictionary<string, string>();

                    for (int j = 1; j < headers.Count && j < cells.Count; j++)
                    {
                        innerDict[headers[j]] = cells[j];
                    }

                    dict[key] = innerDict;
                }
            }

            return dict;
        }

        //private void pathLabel_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show("hihi");
        //}

        private void openNewFile_Click(object sender, EventArgs e)
        {
            string filePath = pathLabel.Text;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Plik nie istnieje :( " + filePath);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie uda³o siê otworzyæ pliku: " + ex.Message);
            }
        }

        private void openCatalogButton_Click(object sender, EventArgs e)
        {
            string filePath = pathLabel.Text;

            if (File.Exists(filePath))
            {
                string argument = "/select, \"" + filePath + "\"";
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            else
            {
                MessageBox.Show("Plik nie istnieje :( " + filePath);
            }
        }
    }
}