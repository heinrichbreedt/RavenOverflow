﻿using System.ComponentModel.DataAnnotations;
using CuttingEdge.Conditions;
using Raven.Client;
using RavenOverflow.Core.Entities;
using RavenOverflow.Core.Services;

namespace RavenOverflow.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IDocumentSession _documentSession;

        public QuestionService(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        #region IQuestionService Members

        public Question Create(Question question)
        {
            Condition.Requires(question).IsNotNull();

            // First, validate the question.
            Validator.ValidateObject(question, new ValidationContext(question, null, null), true);
            Condition.Requires(question.Tags).IsNotNull().IsLongerThan(0);

            // Save.
            _documentSession.Store(question);
            _documentSession.SaveChanges();

            return question;
        }

        #endregion
    }
}