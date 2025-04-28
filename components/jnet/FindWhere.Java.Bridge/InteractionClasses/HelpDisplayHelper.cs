[System.Serializable]

public class HelpDisplayHelper
{

    private string helpContent;

    public HelpDisplayHelper(string helpContent)
    {
        this.helpContent = helpContent;
    }

    public HelpDisplayHelper()
    {
    }

    public string getHelpContent()
    {
        return helpContent;
    }
}