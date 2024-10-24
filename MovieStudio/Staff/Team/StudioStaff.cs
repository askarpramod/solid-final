using System.Collections.Generic;

namespace MovieStudio.Staff.Team
{
    public class StudioStaff
    {
        public List<Actor> Actors { get; private set; }
        public List<CameraMan> Cameramen { get; private set; }

        public StudioStaff()
        {
        }

        public StudioStaff(Actor[] actors,
                               CameraMan[] cameramen)
        {
            this.Actors = new List<Actor>();
            this.Cameramen = new List<CameraMan>();

            foreach (Actor actor in actors)
            {
                AddInitialPersonDefinition(actor);
            }
            foreach (CameraMan cameraMan in cameramen)
            {
                AddInitialPersonDefinition(cameraMan);
            }
        }

        public void AddInitialPersonDefinition(CameraMan cameraMan)
        {
            if (Cameramen == null)
                Cameramen = new List<CameraMan>();
            Cameramen.Add(cameraMan);
        }

        public void AddInitialPersonDefinition(Actor actor)
        {
            if (Actors == null)
                Actors = new List<Actor>();
            Actors.Add(actor);
        }
    }
}
