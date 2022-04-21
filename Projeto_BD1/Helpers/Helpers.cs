
namespace Projeto_BD1.Helpers
{
    public static class HelpersFunctions
    {
        public static T GetFirstObject<T>(IEnumerable<T> arrayObjects) where T : class
        {
            if (arrayObjects is null)
            {
                throw new ArgumentNullException(nameof(arrayObjects));
            }

            if(!arrayObjects.Any() || arrayObjects.Count() <= 0)
            {
                return null;
            }

            return arrayObjects.FirstOrDefault();
        }

    }
}
