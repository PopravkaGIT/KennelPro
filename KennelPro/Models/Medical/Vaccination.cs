using KennelPro.Models.Dogs;

namespace KennelPro.Models.Medical;

public class Vaccination
{
    public Guid Id { get; set; }


    public Guid DogId { get; set; }

    public Dog Dog { get; set; }
        = null!;


    // Например Nobivac + L(R)
    public string Name { get; set; } = string.Empty;


    public DateTime VaccinationDate { get; set; }


    // Следующая вакцинация
    public DateTime? RevaccinationDate { get; set; }
}