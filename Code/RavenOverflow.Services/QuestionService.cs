﻿using System.ComponentModel.DataAnnotations;
using CuttingEdge.Conditions;
using Raven.Client;
using RavenOverflow.Core.Entities;
using RavenOverflow.Core.Services;

namespace RavenOverflow.Services
{
    public class QuestionService : IQuestionService
    {
        #region IQuestionService Members

        public Question Create(Question question, IDocumentSession documentSession)
        {
            Condition.Requires(question).IsNotNull();
            Condition.Requires(documentSession).IsNotNull();

            // First, validate the question.
            Validator.ValidateObject(question, new ValidationContext(question, null, null), true);
            Condition.Requires(question.Tags).IsNotNull().IsLongerThan(0);

            // Save.
            documentSession.Store(question);

            return question;
        }

        #endregion
    }
}