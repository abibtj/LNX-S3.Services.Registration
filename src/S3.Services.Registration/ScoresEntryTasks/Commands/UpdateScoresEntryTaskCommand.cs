using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;
using S3.Services.Registration.Domain;
using System.Collections.Generic;

namespace S3.Services.Registration.ScoresEntryTasks.Commands
{
    public class UpdateScoresEntryTaskCommand : ICommand
    {
        [Required]
        public Guid Id { get; }
        [Required]
        public Guid SchoolId { get; }
        [Required]
        public Guid TeacherId { get; }
        [Required(ErrorMessage = "Teacher's name is required.")]
        public string TeacherName { get; }
        [Required]
        public Guid ClassId { get; }

        [Required(ErrorMessage = "Class' name is required.")]
        public string ClassName { get; }
        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; }
        public DateTime? DueDate { get; }
        public string? Description { get; }
        [Required]
        public Guid RuleId { get; }

        [JsonConstructor]
        public UpdateScoresEntryTaskCommand(Guid id, Guid schoolId, Guid teacherId, string teacherName, Guid classId,
            string className, string subject, DateTime? dueDate, string? description, Guid ruleId)

            => (Id, SchoolId, TeacherId, TeacherName, ClassId, ClassName, Subject, DueDate, Description, RuleId)
            = (id, schoolId, teacherId, teacherName, classId, className, subject, dueDate, description, ruleId);
    }
}