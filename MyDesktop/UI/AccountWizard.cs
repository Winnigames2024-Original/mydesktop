using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyDesktop.Core;

namespace MyDesktop.UI
{
    public partial class AccountWizard : Form
    {
        private readonly AppConfig _cfg;
        private readonly ApiClient _api;

        public string CreatedLogin { get; private set; }

        // Для конструктора форм Visual Studio.
        public AccountWizard()
        {
            InitializeComponent();
        }

        public AccountWizard(AppConfig cfg, bool first) : this()
        {
            _cfg = cfg;
            _api = new ApiClient(cfg);
            if (first)
            {
                lblTitle.Text = "Первый аккаунт";
                lblSub.Text = "Это будет администратор.";
                this.Text = "MyDesktop — первый аккаунт";
            }
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (_api == null) return;
            var login = txtLogin.Text.Trim();
            var p1 = txtPass.Text;
            var p2 = txtPass2.Text;
            if (login.Length < 3) { Err("Логин минимум 3 символа."); return; }
            if (p1.Length < 6) { Err("Пароль минимум 6 символов."); return; }
            if (p1 != p2) { Err("Пароли не совпадают."); return; }

            btnCreate.Enabled = false; btnCreate.Text = "Создаю…";
            var r = await _api.RegisterAsync(login, p1);
            btnCreate.Enabled = true; btnCreate.Text = "Создать";

            if (r.Ok)
            {
                CreatedLogin = login;
                DialogResult = DialogResult.OK;
                Close();
            }
            else Err(r.Message ?? "Не удалось создать аккаунт.");
        }

        private void Err(string t)
        {
            lblMsg.ForeColor = System.Drawing.Color.FromArgb(255, 91, 110);
            lblMsg.Text = t;
        }
    }
}
