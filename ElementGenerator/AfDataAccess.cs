using OSIsoft.AF;
using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ElementGenerator
{
    public class AfDataAccess
    {

        private readonly NetworkCredential _networkCredential;       
        
        private readonly PISystem _piSystem;
        private readonly AFDatabase _afDatabase;

        public AfDataAccess(string afServer, string afDatabase, string userName, string password)
        {
            var networkCredential = new NetworkCredential(userName, password);
            _piSystem = new PISystems()[afServer];
            _piSystem.Connect(networkCredential);
            _afDatabase = _piSystem.Databases[afDatabase];
                       
        }

        public void CreateElement(string name)
        {
            var template = AfDatabase.ElementTemplates["Gas Compressor"];
            
            if(AfDatabase.Elements[name] == null)
            {
                var newElement = AfDatabase.Elements.Add(name, template);
                newElement.Attributes["Facility ID"].SetValue(new AFValue(name));
                AFDataReference.CreateConfig(newElement, true, null);
                AfDatabase.CheckIn();
            }
            
        }

        public void AddValues(IEnumerable<AFValues> afValuesCollection)
        { 

            AFValues afValues = new AFValues();
            
            foreach (var afValuesItem in afValuesCollection)
            {
                foreach (var afValue in afValuesItem)
                {
                    afValues.Add(afValue);
                }
            }
        
            var returnErrors = AFListData.UpdateValues(afValues, AFUpdateOption.Insert);
            
        }

        public AFElement GetElement(string name)
        {
            return _afDatabase.Elements[name];
        }

        public AFDatabase AfDatabase
        {
            get
            {
                return _afDatabase;
            }
        }

    }
}
