using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WindowsRepairTools
{
    public class MainForm : Form
    {
        private Button btnResetWU;
        private Button btnSFC;
        private Button btnDISM;
        private Button btnTemp;
        private Button btnWinUpdate;
        private Button btnProgUpdate;
        private Button btnChkdsk;
        private Button btnDns;
        private Button btnBoot;
        private Button btnRegistry;
        private Button btnStore;
        private Button btnDrivers;
        private TextBox txtLog;
        private ProgressBar progressBar;
        private Label lblExecuting;

        private Image iconWU;
        private Image iconSFC;
        private Image iconDISM;
        private Image iconTemp;
        private Image iconWinUpdate;
        private Image iconProgUpdate;
        private Image iconChkdsk;
        private Image iconDns;
        private Image iconBoot;
        private Image iconRegistry;
        private Image iconStore;
        private Image iconDrivers;

        private readonly Color Bg = Color.FromArgb(20, 24, 30);
        private readonly Color Surface = Color.FromArgb(28, 34, 42);
        private readonly Color SurfaceLight = Color.FromArgb(36, 43, 52);
        private readonly Color Accent = Color.FromArgb(0, 173, 181);
        private readonly Color TextPrimary = Color.FromArgb(238, 238, 238);
        private readonly Color TextMuted = Color.FromArgb(176, 190, 197);

        public MainForm()
        {
            this.Text = "Windows Repair tools";
            this.Width = 1040;
            this.Height = 640;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.BackColor = Bg;
            this.Font = new Font("Segoe UI", 10);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Padding = new Padding(16, 12, 16, 12);

            var titleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 34,
                BackColor = Bg
            };

            var lblTitleBar = new Label
            {
                Text = "Windows Repair tools",
                Dock = DockStyle.Left,
                Width = 220,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = TextPrimary,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                Padding = new Padding(8, 0, 0, 0)
            };

            var btnClose = new Button
            {
                Text = "X",
                Dock = DockStyle.Right,
                Width = 46,
                Height = 26,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(200, 60, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => Close();

            var btnMin = new Button
            {
                Text = "-",
                Dock = DockStyle.Right,
                Width = 46,
                Height = 26,
                FlatStyle = FlatStyle.Flat,
                BackColor = SurfaceLight,
                ForeColor = TextPrimary,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.Click += (s, e) => WindowState = FormWindowState.Minimized;

            titleBar.Controls.Add(btnClose);
            titleBar.Controls.Add(btnMin);
            titleBar.Controls.Add(lblTitleBar);

            titleBar.MouseDown += (s, e) => BeginDrag(e);
            lblTitleBar.MouseDown += (s, e) => BeginDrag(e);

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = Bg
            };

            var lblTitle = new Label
            {
                Text = "Windows Repair tools",
                Dock = DockStyle.Top,
                Height = 36,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Accent,
                Padding = new Padding(6, 2, 0, 0)
            };

            var lblSubtitle = new Label
            {
                Text = "Correções rápidas e atualizações essenciais",
                Dock = DockStyle.Top,
                Height = 22,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = TextMuted,
                Padding = new Padding(8, 0, 0, 0)
            };

            header.Controls.Add(lblSubtitle);
            header.Controls.Add(lblTitle);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Bg,
                Padding = new Padding(0, 8, 0, 0)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55));

            var leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Surface,
                Padding = new Padding(16)
            };

            var rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Surface,
                Padding = new Padding(16)
            };

            var lblActions = new Label
            {
                Text = "Ações rápidas",
                Dock = DockStyle.Top,
                Height = 26,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = TextPrimary
            };

            var tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Padding = new Point(12, 8)
            };

            // Tab Reparos
            var tabRepair = new TabPage("Reparos")
            {
                BackColor = Surface,
                Padding = new Padding(12)
            };
            var repairFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 4, 0, 0)
            };

            // Tab Limpeza
            var tabClean = new TabPage("Limpeza")
            {
                BackColor = Surface,
                Padding = new Padding(12)
            };
            var cleanFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 4, 0, 0)
            };

            // Tab Atualizações
            var tabUpdate = new TabPage("Atualizações")
            {
                BackColor = Surface,
                Padding = new Padding(12)
            };
            var updateFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 4, 0, 0)
            };

            iconWU = CreateIcon("wrench", Accent);
            iconSFC = CreateIcon("scan", Color.FromArgb(66, 165, 245));
            iconDISM = CreateIcon("shield", Color.FromArgb(255, 167, 38));
            iconTemp = CreateIcon("broom", Color.FromArgb(156, 204, 101));
            iconWinUpdate = CreateIcon("download", Color.FromArgb(38, 198, 218));
            iconProgUpdate = CreateIcon("upload", Color.FromArgb(171, 71, 188));
            iconChkdsk = CreateIcon("disk", Color.FromArgb(244, 67, 54));
            iconDns = CreateIcon("globe", Color.FromArgb(76, 175, 80));
            iconBoot = CreateIcon("boot", Color.FromArgb(33, 150, 243));
            iconRegistry = CreateIcon("registry", Color.FromArgb(233, 30, 99));
            iconStore = CreateIcon("store", Color.FromArgb(156, 39, 176));
            iconDrivers = CreateIcon("drivers", Color.FromArgb(255, 112, 67));

            btnResetWU = new Button { Text = "Resetar Windows Update", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnSFC = new Button { Text = "Executar SFC", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnDISM = new Button { Text = "Executar DISM", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnBoot = new Button { Text = "Reparar Boot", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnRegistry = new Button { Text = "Limpar Registro", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 0) };

            btnTemp = new Button { Text = "Apagar Temporários", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnDns = new Button { Text = "Limpar Cache DNS", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnStore = new Button { Text = "Resetar Microsoft Store", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnChkdsk = new Button { Text = "Verificar Disco (CHKDSK)", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 0) };

            btnWinUpdate = new Button { Text = "Atualizar Sistema (Windows Update)", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnProgUpdate = new Button { Text = "Atualizar Programas (winget)", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 10) };
            btnDrivers = new Button { Text = "Atualizar Drivers", Width = 340, Height = 46, Margin = new Padding(0, 0, 0, 0) };

            btnResetWU.Image = iconWU;
            btnSFC.Image = iconSFC;
            btnDISM.Image = iconDISM;
            btnTemp.Image = iconTemp;
            btnWinUpdate.Image = iconWinUpdate;
            btnProgUpdate.Image = iconProgUpdate;
            btnChkdsk.Image = iconChkdsk;
            btnDns.Image = iconDns;
            btnBoot.Image = iconBoot;
            btnRegistry.Image = iconRegistry;
            btnStore.Image = iconStore;
            btnDrivers.Image = iconDrivers;

            foreach (var btn in new[] { btnResetWU, btnSFC, btnDISM, btnBoot, btnRegistry })
            {
                ApplyButtonStyle(btn);
                repairFlow.Controls.Add(btn);
            }

            foreach (var btn in new[] { btnTemp, btnDns, btnStore, btnChkdsk })
            {
                ApplyButtonStyle(btn);
                cleanFlow.Controls.Add(btn);
            }

            foreach (var btn in new[] { btnWinUpdate, btnProgUpdate, btnDrivers })
            {
                ApplyButtonStyle(btn);
                updateFlow.Controls.Add(btn);
            }

            tabRepair.Controls.Add(repairFlow);
            tabClean.Controls.Add(cleanFlow);
            tabUpdate.Controls.Add(updateFlow);

            tabControl.TabPages.Add(tabRepair);
            tabControl.TabPages.Add(tabClean);
            tabControl.TabPages.Add(tabUpdate);

            var lblLogTitle = new Label
            {
                Text = "Log de execução",
                Dock = DockStyle.Top,
                Height = 26,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = TextPrimary
            };

            progressBar = new ProgressBar
            {
                Width = 120,
                Height = 8,
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 30,
                Visible = false
            };

            lblExecuting = new Label
            {
                Text = "executando",
                AutoSize = true,
                ForeColor = TextMuted,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                Visible = false,
                Margin = new Padding(8, 0, 0, 0)
            };

            var progressRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 20,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(2, 4, 0, 0)
            };
            progressRow.Controls.Add(progressBar);
            progressRow.Controls.Add(lblExecuting);

            var logContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SurfaceLight,
                Padding = new Padding(10, 8, 10, 10)
            };

            txtLog = new TextBox
            {
                Multiline = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                BackColor = SurfaceLight,
                ForeColor = TextPrimary,
                Font = new Font("Consolas", 10),
                ReadOnly = true,
                BorderStyle = BorderStyle.None
            };

            logContainer.Controls.Add(txtLog);

            leftPanel.Controls.Add(tabControl);
            leftPanel.Controls.Add(lblActions);

            rightPanel.Controls.Add(logContainer);
            rightPanel.Controls.Add(progressRow);
            rightPanel.Controls.Add(lblLogTitle);

            layout.Controls.Add(leftPanel, 0, 0);
            layout.Controls.Add(rightPanel, 1, 0);

            btnResetWU.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.ResetWindowsUpdateAsync(Log));
            btnSFC.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.RunSfcAsync(Log));
            btnDISM.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.RunDismAsync(Log));
            btnTemp.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.ClearTempAsync(Log));
            btnWinUpdate.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.UpdateWindowsAsync(Log));
            btnProgUpdate.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.UpdateProgramsAsync(Log));
            btnChkdsk.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.CheckDiskAsync(Log));
            btnDns.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.FlushDnsAsync(Log));
            btnBoot.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.RepairBootAsync(Log));
            btnRegistry.Click += async (s, e) => await RunRegistryCleanupAsync();
            btnStore.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.ResetWindowsStoreAsync(Log));
            btnDrivers.Click += async (s, e) => await RunWithBusy(() => Program.RepairService.UpdateDriversAsync(Log));

            Controls.Add(layout);
            Controls.Add(header);
            Controls.Add(titleBar);

            Shown += (s, e) => ApplyRoundedCorners();
            Resize += (s, e) => ApplyRoundedCorners();
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        private void ApplyRoundedCorners()
        {
            ApplyRoundedRegion(this, 16);
            ApplyRoundedRegionToChildren(this);
        }

        private void ApplyRoundedRegionToChildren(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child is Button)
                {
                    ApplyRoundedRegion(child, 10);
                }
                else if (child is Panel || child is TabControl || child is TabPage || child is TextBox)
                {
                    ApplyRoundedRegion(child, 12);
                }

                if (child.HasChildren)
                {
                    ApplyRoundedRegionToChildren(child);
                }
            }
        }

        private void ApplyRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0)
            {
                return;
            }

            IntPtr region = CreateRoundRectRgn(0, 0, control.Width + 1, control.Height + 1, radius, radius);
            control.Region = Region.FromHrgn(region);
            DeleteObject(region);
        }
        private void ApplyButtonStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = SurfaceLight;
            btn.ForeColor = TextPrimary;
            btn.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.Image = btn.Image;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(48, 58, 70);
            btn.MouseLeave += (s, e) => btn.BackColor = SurfaceLight;
        }

        private Image CreateIcon(string type, Color color)
        {
            var bmp = new Bitmap(28, 28);
            using (var g = Graphics.FromImage(bmp))
            using (var brush = new SolidBrush(color))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                g.FillEllipse(brush, 0, 0, 28, 28);
                DrawIconGlyph(g, type);
            }

            return bmp;
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private void BeginDrag(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private async Task RunRegistryCleanupAsync()
        {
            await RunWithBusy(async () =>
            {
                Log($"{DateTime.Now:HH:mm:ss} - Buscando entradas invalidas de programas...\r\n");
                var invalidEntries = await Program.RepairService.FindInvalidProgramEntriesAsync(Log);

                if (invalidEntries.Count == 0)
                {
                    Log("Nenhuma entrada invalida encontrada.\r\n");
                    MessageBox.Show("Nenhuma entrada invalida foi encontrada.", "Limpar Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await Program.RepairService.CleanRegistryMruAsync(Log);
                    return;
                }

                using var dialog = new InvalidProgramsDialog(invalidEntries, Surface, SurfaceLight, TextPrimary, TextMuted, Accent);
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    Log("Operacao cancelada pelo usuario.\r\n");
                    return;
                }

                var selected = dialog.SelectedEntries;
                if (selected.Count == 0)
                {
                    Log("Nenhuma entrada selecionada para remocao.\r\n");
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Remover {selected.Count} entradas invalidas do registro?",
                    "Confirmar remocao",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                {
                    Log("Remocao cancelada pelo usuario.\r\n");
                    return;
                }

                await Program.RepairService.RemoveInvalidProgramEntriesAsync(selected, Log);
                await Program.RepairService.CleanRegistryMruAsync(Log);
            });
        }

        private void DrawIconGlyph(Graphics g, string type)
        {
            using (var pen = new Pen(Color.White, 2.2f))
            using (var brush = new SolidBrush(Color.White))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                switch (type)
                {
                    case "wrench":
                        g.DrawArc(pen, 6, 6, 10, 10, 200, 280);
                        g.DrawLine(pen, 13, 13, 21, 21);
                        g.FillEllipse(brush, 20, 20, 4, 4);
                        break;
                    case "scan":
                        g.DrawEllipse(pen, 6, 6, 12, 12);
                        g.DrawLine(pen, 16, 16, 22, 22);
                        break;
                    case "shield":
                        var shield = new PointF[]
                        {
                            new PointF(14, 5),
                            new PointF(22, 8),
                            new PointF(20, 19),
                            new PointF(14, 23),
                            new PointF(8, 19),
                            new PointF(6, 8)
                        };
                        g.FillPolygon(brush, shield);
                        break;
                    case "broom":
                        g.DrawLine(pen, 8, 8, 20, 20);
                        g.FillPolygon(brush, new PointF[]
                        {
                            new PointF(18, 14),
                            new PointF(24, 20),
                            new PointF(20, 24)
                        });
                        break;
                    case "download":
                        g.DrawLine(pen, 14, 6, 14, 17);
                        g.FillPolygon(brush, new PointF[]
                        {
                            new PointF(9, 14),
                            new PointF(14, 20),
                            new PointF(19, 14)
                        });
                        g.DrawLine(pen, 8, 22, 20, 22);
                        break;
                    case "upload":
                        g.DrawLine(pen, 14, 22, 14, 11);
                        g.FillPolygon(brush, new PointF[]
                        {
                            new PointF(9, 14),
                            new PointF(14, 8),
                            new PointF(19, 14)
                        });
                        g.DrawLine(pen, 8, 22, 20, 22);
                        break;
                    case "disk":
                        g.DrawEllipse(pen, 6, 8, 16, 12);
                        g.FillEllipse(brush, 10, 13, 8, 4);
                        break;
                    case "globe":
                        g.DrawEllipse(pen, 6, 6, 16, 16);
                        g.DrawLine(pen, 14, 6, 14, 22);
                        g.DrawArc(pen, 6, 8, 16, 12, 0, 360);
                        break;
                    case "boot":
                        g.DrawRectangle(pen, 8, 6, 12, 14);
                        g.DrawLine(pen, 8, 12, 20, 12);
                        g.FillRectangle(brush, 10, 20, 8, 2);
                        break;
                    case "registry":
                        g.DrawRectangle(pen, 6, 8, 16, 12);
                        g.DrawLine(pen, 14, 8, 14, 20);
                        g.DrawLine(pen, 6, 14, 22, 14);
                        break;
                    case "store":
                        g.DrawRectangle(pen, 7, 7, 14, 14);
                        g.DrawLine(pen, 10, 7, 10, 21);
                        g.DrawLine(pen, 18, 7, 18, 21);
                        break;
                    case "drivers":
                        g.DrawRectangle(pen, 8, 9, 12, 10);
                        g.DrawLine(pen, 10, 9, 10, 5);
                        g.DrawLine(pen, 18, 9, 18, 5);
                        g.FillRectangle(brush, 10, 4, 1, 2);
                        g.FillRectangle(brush, 17, 4, 1, 2);
                        break;
                }
            }
        }

        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => txtLog.AppendText(message)));
                return;
            }

            txtLog.AppendText(message);
        }

        private sealed class InvalidProgramsDialog : Form
        {
            private readonly ListView listView;
            private readonly Button btnOk;
            private readonly Button btnCancel;
            private readonly Button btnSelectAll;
            private readonly Button btnClear;

            public InvalidProgramsDialog(
                IReadOnlyList<Program.RepairService.InvalidProgramEntry> entries,
                Color surface,
                Color surfaceLight,
                Color textPrimary,
                Color textMuted,
                Color accent)
            {
                Text = "Entradas invalidas encontradas";
                Width = 760;
                Height = 420;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                MinimizeBox = false;
                StartPosition = FormStartPosition.CenterParent;
                BackColor = surface;
                Font = new Font("Segoe UI", 9.5f);

                var lblInfo = new Label
                {
                    Text = "Selecione as entradas invalidas para remover:",
                    Dock = DockStyle.Top,
                    Height = 28,
                    ForeColor = textPrimary,
                    Padding = new Padding(6, 6, 0, 0)
                };

                listView = new ListView
                {
                    Dock = DockStyle.Fill,
                    CheckBoxes = true,
                    View = View.Details,
                    FullRowSelect = true,
                    GridLines = false,
                    BorderStyle = BorderStyle.None,
                    BackColor = surfaceLight,
                    ForeColor = textPrimary
                };

                listView.Columns.Add("Programa", 280, HorizontalAlignment.Left);
                listView.Columns.Add("Motivo", 220, HorizontalAlignment.Left);
                listView.Columns.Add("Origem", 140, HorizontalAlignment.Left);

                foreach (var entry in entries)
                {
                    var item = new ListViewItem(entry.DisplayName);
                    item.SubItems.Add(entry.Reason);
                    item.SubItems.Add(entry.Source);
                    item.Tag = entry;
                    listView.Items.Add(item);
                }

                var buttonRow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 44,
                    FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(8, 6, 8, 6)
                };

                btnOk = new Button
                {
                    Text = "Remover",
                    Width = 110,
                    Height = 28,
                    BackColor = accent,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnOk.FlatAppearance.BorderSize = 0;
                btnOk.Click += (s, e) => DialogResult = DialogResult.OK;

                btnCancel = new Button
                {
                    Text = "Cancelar",
                    Width = 110,
                    Height = 28,
                    BackColor = surfaceLight,
                    ForeColor = textPrimary,
                    FlatStyle = FlatStyle.Flat
                };
                btnCancel.FlatAppearance.BorderSize = 0;
                btnCancel.Click += (s, e) => DialogResult = DialogResult.Cancel;

                btnSelectAll = new Button
                {
                    Text = "Selecionar tudo",
                    Width = 130,
                    Height = 28,
                    BackColor = surfaceLight,
                    ForeColor = textPrimary,
                    FlatStyle = FlatStyle.Flat
                };
                btnSelectAll.FlatAppearance.BorderSize = 0;
                btnSelectAll.Click += (s, e) =>
                {
                    foreach (ListViewItem item in listView.Items)
                    {
                        item.Checked = true;
                    }
                };

                btnClear = new Button
                {
                    Text = "Limpar selecao",
                    Width = 130,
                    Height = 28,
                    BackColor = surfaceLight,
                    ForeColor = textPrimary,
                    FlatStyle = FlatStyle.Flat
                };
                btnClear.FlatAppearance.BorderSize = 0;
                btnClear.Click += (s, e) =>
                {
                    foreach (ListViewItem item in listView.Items)
                    {
                        item.Checked = false;
                    }
                };

                buttonRow.Controls.Add(btnOk);
                buttonRow.Controls.Add(btnCancel);
                buttonRow.Controls.Add(btnClear);
                buttonRow.Controls.Add(btnSelectAll);

                Controls.Add(listView);
                Controls.Add(buttonRow);
                Controls.Add(lblInfo);
            }

            public List<Program.RepairService.InvalidProgramEntry> SelectedEntries
            {
                get
                {
                    return listView.CheckedItems
                        .Cast<ListViewItem>()
                        .Select(item => (Program.RepairService.InvalidProgramEntry)item.Tag)
                        .ToList();
                }
            }
        }

        private async Task RunWithBusy(Func<Task> action)
        {
            SetBusy(true);
            try
            {
                await action();
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void SetBusy(bool isBusy)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetBusy(isBusy)));
                return;
            }

            progressBar.Visible = isBusy;
            lblExecuting.Visible = isBusy;
            btnResetWU.Enabled = !isBusy;
            btnSFC.Enabled = !isBusy;
            btnDISM.Enabled = !isBusy;
            btnTemp.Enabled = !isBusy;
            btnWinUpdate.Enabled = !isBusy;
            btnProgUpdate.Enabled = !isBusy;
            btnChkdsk.Enabled = !isBusy;
            btnDns.Enabled = !isBusy;
            btnBoot.Enabled = !isBusy;
            btnRegistry.Enabled = !isBusy;
            btnStore.Enabled = !isBusy;
            btnDrivers.Enabled = !isBusy;
        }
    }
}