using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace NRobot.Server.Imp.Domain
{
	/// <summary>
	/// Encapsulates a keyword as a method and documentation
	/// </summary>
	public class Keyword
	{

		public MethodInfo KeywordMethod {get; set;}
        public String KeywordDocumentation { get; set; }
        public String FriendlyName { get; set; }
        public string[] ArgumentNames { get; set; }
        public Object ClassInstance { get; set; }

		
		/// <summary>
		/// Get the number of arguments to the keyword
		/// </summary>
		public int ArgumentCount
		{
			get
			{
                return ArgumentNames.Length;
			}
		}

	    public Keyword() {}

		/// <summary>
		/// Constructor from method
		/// </summary>
		public Keyword(Object classinstance, MethodInfo method, XDocument documentation)
		{
            if (classinstance == null) throw new Exception("No class instance specified");
            if (method==null) throw new Exception("No keyword method specified");
			//record properties
            KeywordMethod = method;
			KeywordDocumentation = String.Empty;
            FriendlyName = GenerateFriendlyName(KeywordMethod.Name);
		    ClassInstance = classinstance;
            //get argument names
	﻿  ﻿  ﻿  	ParameterInfo[] pis = KeywordMethod.GetParameters();
	﻿  ﻿  ﻿  	ArgumentNames = new String[pis.Length];
	﻿  ﻿  ﻿   	int i = 0;
	﻿  ﻿  ﻿  	foreach (ParameterInfo pi in pis)
	﻿  ﻿  ﻿  	{
	﻿  ﻿  ﻿  ﻿  	ArgumentNames[i++] = pi.Name;
	﻿  ﻿  ﻿  	}
            //get xml documentation
            if (documentation!=null)
            {
                KeywordDocumentation = method.GetXmlDocumentation(documentation);
            }  
		}

        private static readonly Regex regexCamelCased = new Regex(@"([A-Z])([A-Z])([a-z])|([a-z])([A-Z])");
        /// <summary>
        /// Support camel cased method names with underscores i.e. 
        /// SomeMethod_withUnderscores(string someParameter) => Some Method with Underscores(someParameter)
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public string GenerateFriendlyName(string methodName)
        {
            var result = methodName.Replace("_", " ");
            return regexCamelCased.Replace(result, "$1$4 $2$3$5");
        }
    }
}
