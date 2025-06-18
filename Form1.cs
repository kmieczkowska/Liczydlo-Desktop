using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
            using (var ms = new MemoryStream(Properties.Resources.cat))
            {
                pictureBox1.Image = Image.FromStream(ms);
            }
            toolTip1.SetToolTip(button1, "To bêdzie plik bazowy");
            toolTip1.SetToolTip(button2, "Ten plik do³¹czysz do bazowego - jeœli ma nowe kolumny, to bêd¹ na koñcu");
            toolTip1.SetToolTip(button3, "Scalenie dwóch plików, zada pytanie gdzie zapisaæ scalony plik");
            toolTip1.SetToolTip(button2, "Otwiera folder z nowym plikiem");
            toolTip1.SetToolTip(button2, "Otwiera nowy plik w aplikacji Excel");
            toolTip1.SetToolTip(pictureBox1, "Implementacja: Klaudia Mieczkowska");

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

                var allPeople = new HashSet<string>(data1.Keys);
                allPeople.UnionWith(data2.Keys);

                var allColumns = new HashSet<string>(
                    data1.Values.SelectMany(d => d.Keys)
                    .Concat(data2.Values.SelectMany(d => d.Keys))
                );

                // usuñ kolumny ID
                var idsToRemove = allColumns.Where(c => c.ToLower() == "id").ToList();
                foreach (var col in idsToRemove)
                    allColumns.Remove(col);

                DataTable mergedTable = new DataTable();
                mergedTable.Columns.Add("ID");
                mergedTable.Columns.Add("Name");

                foreach (var col in allColumns)
                {
                    if (col.Equals("title", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!mergedTable.Columns.Contains("Title1"))
                            mergedTable.Columns.Add("Title1");
                        if (!mergedTable.Columns.Contains("Title2"))
                            mergedTable.Columns.Add("Title2");
                    }
                    else
                    {
                        mergedTable.Columns.Add(col);
                    }
                }

                int idCounter = 0;
                foreach (var person in allPeople.OrderBy(p => p))
                {
                    var row = mergedTable.NewRow();
                    row["ID"] = idCounter++.ToString();
                    row["Name"] = person;

                    void WstawDane(Dictionary<string, string> dane)
                    {
                        foreach (var kvp in dane)
                        {
                            string key = kvp.Key.Trim();
                            string value = kvp.Value?.Trim();

                            if (idsToRemove.Contains(key)) continue;

                            if (key.Equals("title", StringComparison.OrdinalIgnoreCase))
                            {
                                if (!string.IsNullOrEmpty(value))
                                {
                                    if (value.Length <= 12)
                                        row["Title1"] = value;
                                    else
                                        row["Title2"] = value;
                                }
                            }
                            else
                            {
                                row[key] = value;
                            }
                        }
                    }

                    if (data1.ContainsKey(person))
                        WstawDane(data1[person]);
                    if (data2.ContainsKey(person))
                        WstawDane(data2[person]);

                    mergedTable.Rows.Add(row);
                }

                SaveFileDialog sfd = new SaveFileDialog();
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

                    pathLabel.Text = sfd.FileName;
                    pathLabel.ForeColor = Color.DeepPink;
                    MessageBox.Show("Scalanie zakoñczone sukcesem", "Sukces");
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
                if (rows.Count == 0) return dict;

                var headers = rows[0].Cells().Select(c => c.Value.ToString().Trim()).ToList();

                // znajdŸ indeks kolumny Name lub Submitter
                int nameIndex = headers.FindIndex(h => h.Equals("Name", StringComparison.OrdinalIgnoreCase)
                                                     || h.Equals("Speakers", StringComparison.OrdinalIgnoreCase));
                if (nameIndex == -1)
                    throw new Exception("Nie znaleziono kolumny Name lub Speakers");

                for (int i = 1; i < rows.Count; i++)
                {
                    var cells = rows[i].Cells().Select(c => c.Value.ToString()).ToList();
                    if (cells.Count <= nameIndex) continue;

                    string key = cells[nameIndex].Trim();
                    if (string.IsNullOrWhiteSpace(key)) continue;

                    var innerDict = new Dictionary<string, string>();
                    for (int j = 0; j < headers.Count && j < cells.Count; j++)
                    {
                        if (j == nameIndex) continue;
                        var header = headers[j];
                        if (!string.IsNullOrWhiteSpace(header))
                            innerDict[header.Trim()] = cells[j];
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