public class CompanyRole {
    public static readonly int SECTIONS = 14;

    private Company company;
    private string name;
    private byte[] accessLevels;

    public CompanyRole(Company company, string name, params byte[] accessLevels) {
        this.company = company;
        this.name = name;
        this.accessLevels = new byte[SECTIONS];

        int _size = accessLevels.Length > SECTIONS ? SECTIONS : accessLevels.Length;
        for(int i = 0; i < _size; i++) {
            this.accessLevels[i] = accessLevels[i];
        }
    }

    public bool checkAccess(int section, int minRequirement) {
        return accessLevels[section] >= minRequirement;
    }
    
    // General access
    // Defenses
    // Armory
    // Hangar
    // Hydroponics
    // Atmosphere control
    // Engineering
    // Fabrication/Manufacturing
    // Medical
    // Chemistry
    // Navigation
    // Sensors
    // Power & Computing
    // Storage


}