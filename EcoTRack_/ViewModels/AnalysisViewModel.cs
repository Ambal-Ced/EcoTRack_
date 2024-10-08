using System.Collections.Generic; // Ensure you have this using directive
using EcoTRack_.NewModel; // Adjust according to your project structure

namespace EcoTRack_.ViewModels
{
    public class AnalysisViewModel
    {
        // List to hold Electrate data
        public List<Electrate> ElectrateList { get; set; }

        // List to hold Insight data
        public List<Insight> InsightList { get; set; }

        // Constructor
        public AnalysisViewModel()
        {
            // Initialize the lists to avoid null reference exceptions
            ElectrateList = new List<Electrate>();
            InsightList = new List<Insight>();
        }
    }
}
