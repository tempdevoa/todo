namespace Todo.Gateways
{
    public abstract class ResponseStatus(string? error)
    {
        public string? Error { get; } = error;

        public bool HasError => !string.IsNullOrEmpty(Error);

        public class Ok : ResponseStatus
        {
            public Ok() : base(string.Empty)
            {
                
            }
        }

        public class Failed : ResponseStatus
        {
            public Failed() : base("Der Aufruf ist fehlgeschlagen.")
            {

            }
        }

        public class Duplicate : ResponseStatus
        {
            public Duplicate(string resourceName) : base($"Die angegebene Ressource '{resourceName}' existiert bereits.")
            {

            }
        }

        public class NotFound : ResponseStatus
        {
            public NotFound(string resourceName) : base($"Die angegebene Ressource '{resourceName}' wurde nicht gefunden.")
            {

            }
        }
    }
}
