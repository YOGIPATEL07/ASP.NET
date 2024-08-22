namespace assignments.Models
{
    public struct ConsoleList
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Label { get; set; }
        public List<ConsoleList> DropdownItems { get; set; }
    }
}