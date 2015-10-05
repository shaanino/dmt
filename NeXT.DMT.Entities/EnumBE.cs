
namespace NeXT.DMT.Entities
{
    public static class EnumBE
    {
        public enum ServerType : int
        {
            Web = 0,
            WebService,
            Database,
            Workflow,
            Report,
            Batch
        }

        public enum Environment : int
        {
            INT = 0,
            UAT,
            STA,
            PROD
        }

        public enum QualificationOfRequest : int
        {
            Major = 0,
            Medium,
            Minor
        }

        public enum Priority : int
        {
            One = 0,
            Two,
            Three
        }

        public enum RegulatedEnvironment : int
        {
            GxpImpact = 0,
            SOAImpact,
            NoImpact
        }
    }
}
