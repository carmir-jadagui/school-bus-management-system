namespace SBMS.Domain.Models;

public class BoyModel : PersonBaseModel
{
    private string _gender = null!;
    public string Gender
    {
        get => _gender;
        set => _gender = value.ToUpper(); // siempre se guarda en mayúsculas
    }

    public int Age { get; set; }
}