namespace BigBearPlastics
{
    //SRP: Responsible for the creation of all objects.
    public class Factory
    {
        public static ISimulatableServicer? GlobalServicerInstance = null;
        public static ISimulatableServicer GetServicer() {
            if (GlobalServicerInstance == null) {
                GlobalServicerInstance = new FLT(new PriorityQueue<ServiceRequest,int>(), CreateLogger());
            }
            return GlobalServicerInstance;
        }
        public static IMessageLogger CreateLogger() {
            return new MessageLogger();
        }
        public static IPartModel CreatePart(string name) {
            return new PartModel(name);
        }

        public static IContainer CreateContainer(int maxFill) {
            return new ContainerModel(maxFill);
        }

        public static IContainer CreateContainerInverted(int maxFill) {
            return new ContainerModel(maxFill, maxFill);
        }

        public static IJobModel CreateJob(List<IPartModel> parts, int partsRequired, int ppInput, int ppOutput, int ppScrap, double pph) {
            return new JobModel(parts,partsRequired,ppInput,ppOutput,ppScrap,pph);
        }

        public static ISimulatableMachine CreateCNC(int id, int priority, Queue<IJobModel> jobs) {
            IJobModel firstJob = jobs.Dequeue();
            return new CNCModel(id, priority, jobs, firstJob, GetServicer(), CreateLogger(), CreateContainerInverted(firstJob.PartsPerInputContainer),CreateContainer(firstJob.PartsPerOutputContainer),CreateContainer(firstJob.ScrapPerScrapContainer));
        }

        public static List<ISimulatableMachine> CreateAllMachines() {

            Dictionary<int,Queue<IJobModel>> jobs = CreateAllJobs();

            List<ISimulatableMachine> output = new List<ISimulatableMachine>();

            //key = machine id - value = priority level
            Dictionary<int,int> priorities = new Dictionary<int,int> {
                { 1,9 },
                { 2,6 },
                { 3,2 },
                { 4,2 },
                { 5,5 },
                { 6,3 },
                { 7,1 },
                { 8,8 },
                { 9,4 },
                { 10,7 }
            };

            foreach(KeyValuePair<int, Queue<IJobModel>> entry in jobs) {
                output.Add(CreateCNC(entry.Key, priorities[entry.Key], entry.Value));
            }

            return output;
        }

        public static Dictionary<int, Queue<IJobModel>> CreateAllJobs() {
            Dictionary<int,Queue<IJobModel>> allJobs = new Dictionary<int,Queue<IJobModel>>();
            allJobs.Add(1,new Queue<IJobModel>());
            allJobs.Add(2,new Queue<IJobModel>());
            allJobs.Add(3,new Queue<IJobModel>());
            allJobs.Add(4,new Queue<IJobModel>());
            allJobs.Add(5,new Queue<IJobModel>());
            allJobs.Add(6,new Queue<IJobModel>());
            allJobs.Add(7,new Queue<IJobModel>());
            allJobs.Add(8,new Queue<IJobModel>());
            allJobs.Add(9,new Queue<IJobModel>());
            allJobs.Add(10,new Queue<IJobModel>());

            allJobs[1].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("REAR FENDER EXTENSION L/H"),CreatePart("REAR FENDER EXTENSION R/H") },
                50,50,50,20,1));

            allJobs[1].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("RR LH FENDER") },
                60,40,10,40,1));

            allJobs[2].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("FENDER LH"),CreatePart("FENDER RH") },
                120,60,10,40,5));

            allJobs[2].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("FENDER AS LH"),CreatePart("FENDER AS RH") },
                90,200,10,80,20));

            allJobs[3].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("FUSION 4 LH FRONT PANEL") },
                42,5,7,100,1.3));

            allJobs[4].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("FUSION 4 RH FRONT PANEL") },
                42,5,7,100,1.3));

            allJobs[5].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("LH SIDE MOUDLING BLACK"),CreatePart("RH SIDE MOULDING BLACK") },
                120,300,120,1200,20));

            allJobs[5].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("LH SIDE MOUDLING YELLOW"),CreatePart("RH SIDE MOULDING YELLOW") },
                200,300,120,1200,20));

            allJobs[6].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("LH BUMPER END"),CreatePart("RH BUMPER END") },
                50,200,20,100,10));

            allJobs[6].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("LH BUMPER END THEIA"),CreatePart("RH BUMPER END THEIA") },
                50,200,20,100,10));

            allJobs[7].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("ROOF NON AC YELLOW AND BLACK") },
                30,30,10,12,8));
            allJobs[7].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("PMA FRONT CORNER CAP N/S"),CreatePart("PMA FRONT CORNER CAP O/S") },
                80,120,80,200,12));

            allJobs[8].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("LH REAR INFILL PANEL"),CreatePart("RH REAR INFILL PANEL"),CreatePart("LH FORWARD INFILL PANEL"),CreatePart("RH FORWARD INFILL PANEL") },
                50,20,10,40,4));

            allJobs[9].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("FENDER TRIMMED") },
                900,300,50,280,30));

            allJobs[10].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("ADAMO DOOR SPAT") },
                70,140,80,100,20));

            allJobs[10].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("N/S SPACER MOULDING TRIMMED"),CreatePart("O/S SPACER MOULDING TRIMMED") },
                300,200,100,800,30));

            allJobs[10].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("LH LOWER RAIL PACKER"),CreatePart("RH LOWER RAIL PACKER") },
                60,100,30,300,45));

            allJobs[10].Enqueue(CreateJob(
                new List<IPartModel> { CreatePart("RAIN VISOR") },
                25,100,20,120,10));


            return allJobs;
        }
    }
}