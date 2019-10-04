using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using S3.Services.Registration.Dto;

namespace S3.Services.Registration.Students.Queries
{
    public class GetStudentQuery : GetQuery<Student>, IQuery<StudentDto>
    {
        [JsonConstructor]
        public GetStudentQuery(Guid id, string[]? includeArray) : base(id, includeArray) { }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using Microsoft.CodeAnalysis.Scripting;
//using Microsoft.CodeAnalysis.CSharp.Scripting;
//using Newtonsoft.Json;
//using S3.Common.Types;
//using S3.Services.Registration.Domain;
//using S3.Services.Registration.Dto;

//namespace S3.Services.Registration.Students.Queries
//{
//    public class GetStudentQuery : IQuery<StudentDto>
//    {
//        public Guid Id { get; }
//        public Expression<Func<Student, object>>[]? IncludeExpressions { get; }

//        [JsonConstructor]
//        public GetStudentQuery(Guid id, string[]? includeArray)
//        {
//            Id = id;
//            IncludeExpressions = null;

//            if (!(includeArray is null) && includeArray.Length > 0)
//            {
//                var includeExpressions = new List<Expression<Func<Student, object>>>();
//                var options = ScriptOptions.Default.AddReferences(typeof(Startup).Assembly);

//                foreach (var includeItem in includeArray)
//                {
//                    var expressionString = $"x => x.{includeItem}";
//                    try
//                    {
//                        var expressionLambda = CSharpScript.EvaluateAsync<Expression<Func<Student, object>>>(expressionString, options).Result;
//                        includeExpressions.Add(expressionLambda);
//                    }
//                    catch (CompilationErrorException ex)
//                    {
//                        //TODO: Log this exception or throw a customised exception and handle it elsewhere.
//                        //throw;
//                    }

//                }

//                IncludeExpressions = includeExpressions.ToArray();
//            }
//        }
//    }
//}
