namespace qubgrademe_savetodatabase.Controllers
{
    public class AddLatencyRequest
    {
        public int Proxy { get; set; }
        public int ProxyBackup { get; set; }
        public int MaxMin { get; set; }
        public int SortedModules { get; set; }
        public int ClassifyGrade { get; set; }
        public int PercentNeededForFirst { get; set; }
        public int TotalMarks { get; set; }
        public int AverageGrade { get; set; }
        public int Retrieve { get; set; }
        public int SaveToDb { get; set; }
        public string dateSaved { get; set; } = "";

    }
}