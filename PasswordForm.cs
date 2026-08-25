namespace WCRCorder;

public partial class PasswordForm : Form
{
    private readonly string _password;

    public PasswordForm(string password)
    {
        InitializeComponent();

        _password = password;

        AcceptButton = buttonOk;
        CancelButton = buttonCancel;
    }

    public bool IsPasswordValid =>
        textBoxPassword.Text == _password;
}