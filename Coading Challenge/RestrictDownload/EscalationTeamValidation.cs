using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestrictDownload
{
    public class EscalationTeamValidation : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            try
            {
                IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
                IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                QueryExpression query = new QueryExpression("team");
                query.ColumnSet = new ColumnSet(true);
                query.Criteria.AddCondition(new ConditionExpression("name", ConditionOperator.Like, "%Escalation Team%"));
                LinkEntity link = query.AddLink("teammembership", "teamid", "teamid");
                link.LinkCriteria.AddCondition(new ConditionExpression("systemuserid", ConditionOperator.Equal, context.InitiatingUserId));
                var results = service.RetrieveMultiple(query);

                if (results.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException("You are not allow to download the attached documents");
                }
            }
            catch(InvalidPluginExecutionException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
