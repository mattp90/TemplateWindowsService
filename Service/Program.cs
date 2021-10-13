namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            Configurations config = new Configurations();

            Service service = new Service(config);
            ProgramBase program = new ProgramBase(service);
            program.Main(args);
        }
    }
}
