﻿using Laraue.EfCoreTriggers.Common.Builders.Triggers.Base;
using Laraue.EfCoreTriggers.Common.Builders.Providers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Laraue.EfCoreTriggers.Common.Builders.Triggers.OnDelete
{
    public class OnDeleteTriggerUpsertAction<TTriggerEntity, TUpsertEntity> : TriggerUpsertAction<TTriggerEntity, TUpsertEntity>
        where TTriggerEntity : class
        where TUpsertEntity : class
    {
        public OnDeleteTriggerUpsertAction(
            Expression<Func<TUpsertEntity, object>> matchExpression,
            Expression<Func<TTriggerEntity, TUpsertEntity>> insertExpression,
            Expression<Func<TTriggerEntity, TUpsertEntity, TUpsertEntity>> onMatchExpression)
                : base(matchExpression, insertExpression, onMatchExpression)
        {
        }

        public override Dictionary<string, ArgumentType> InsertExpressionPrefixes => new()
        {
            [InsertExpression.Parameters[0].Name] = ArgumentType.Old,
        };

        public override Dictionary<string, ArgumentType> OnMatchExpressionPrefixes => OnMatchExpression is null
            ? new Dictionary<string, ArgumentType>()
            : new Dictionary<string, ArgumentType>
            {
                [OnMatchExpression.Parameters[0].Name] = ArgumentType.Old,
            };

        public override Dictionary<string, ArgumentType> MatchExpressionPrefixes => new()
        {
            [MatchExpression.Parameters[0].Name] = ArgumentType.Old,
        };
    }
}