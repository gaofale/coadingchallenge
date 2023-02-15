# Instructions to set up Managed Solution for the provided requirement
<h3>Pre-Deployment Steps</h3>

1. Create the following three business units
	i.   Underground
	ii.  Buses
	iii. Overground
	
2. Create the following teams (team type owner) in their respective BUs
	i.   Underground Escalation Team
	ii.  Underground Confidential Case Team
	iii. Overground Escalation Team
	iv.  Overground Confidential Case Team
	v.   Buses Escalation Team
	vi.  Buses Confidential Case Team
  
<h3>Deploy the Managed Solution</h3>
Deploy CoadingChallenge_1_0_0_0_managed.zip file to CRM
While Import It will Ask for connection do provide or create connection from System Admin Account.


<h3>Post-Deployment Steps</h3>
1. All the Team User need to have "Basic User" Security Role to access the org.

2. Assign "Team Security Role" to all the above created teams

3. Assign "Customer Service Agent" Security role to Agents of each BU

4. Assign "Customer Service Manager" Security role to Managers of each BU

5. Share Document Canvas App to Escalation Team members

6. Turn On any PowerAutomate flow from the solution if it is in deactivated stag

<h3>Assumptions</h3>

1. Agents are only going to upload photos into the attachments of Case, Contact, and Activities.

<h3>Solution</h3>

My provided solution consists of the following components

1. Customer Service Hub Model driven App
	I have used an out-of-the-box Customer Service Hub App that needed to be configured to add some forms and Security Roles.
2. Below are the Entities which are configured
	<h4>Case</h4>
		i.  Field
			 tfl_isconfidential | Is Confidential | Two Option   (To Classify the Case)
		ii. Forms
			Case For Interactive experience | Main (Added above field on the Form and removed some not related grids)
			Case Quick Create | Quick Create Form ( Added above field for agent to quickly create Case from Contact itself)
			Issue Snapshot | Quick View Form (Not Modified used as is on Follow Up Task Form of Task Entity to have Case details on Follow Up Task itself)
	<h4>Contact</h4>
		i. Forms
			Contact For Interactive experience | Main (Removed some not related grids)
			Contact Quick Form (Used as is on Follow Up Task Form of Task Entity to have Contact details on Follow Up Activity Task itself)
	
	<h4>Task (Used Task Entity as Follow Up Activity)</h4>
		i. Field
			tfl_isfollowup |  Is Follow Up? | Two Option  (To seperate normal task with follow up Task)
			tfl_contact    | Contact		| LookUp	  (For getting Contact information on Follow Up Task Form)
		ii.Forms
			Task Quick Create Form | Quick Create Form (Added above fields for Manager to create Follow Up Activity)
			Follow Up Task | Main 
			(New Form created for Escalation & Confidential Teams. Added Case and Contact Quick view to have Case and Contact information on one place
			Also on Document Tab of this Form Added Embeded Canvas App to view all the documents at one place.)
			Task For Interactive experience | Main ( Task Main Form for every one)
			
3. Below are the Customization done to implement provided requirnment
	
	<h4>WebResource</h4>
	tfl_caseribbion 
		Added to show and hide "Resolve Case" button for Managers and others respectively.
	
	<h4>Plugin</h4>
	RestrictDownload.EscalationTeamValidation
		This plugin is registered on the Retrieve Message of the Annotation entity at the Pre-Validation step. 
		If a user is a member of the Escalation team, the plugin throws an exception, not allowing the user to proceed. 
	
	<h4>Processes</h4>
	Assign Follow Up Task to Team | Power Automate
		This workflow assigns follow-up tasks to the Escalation & Confidential teams based on the task's owning Business Unit, as well as whether it is classified. Also Share the Case Record with the Team for Case Data Access. 
		
	GetDocuments | Power Automate
		This flow is called from the Canvas app to retrieve all the attachments from Notes as well as Activity mime attachments.
	
	<h4>Canvas App</h4>
	Document App
		This Canvas app shows all the documents in one place on the Follow-Up Task 
		
4. Below are the Security Roles which has been configured

	i.   Customer Service Agent (Has User Level access to Case and Activity, BU Level to Contact)
	ii.  Customer Service Manager (Has BU Level access to Case, Contact and Activity)
	iii. Team Security Role (Has User Level access on Case and Activity, Bu Level to Contact as well as access to the Canvas app entity)

<h3>Test Scenario </h3>


<h4>Pre-conditions:</h4>

User has the necessary security role (Customer Service Agent/Manager/Team Security Role).
The plugins and workflows mentioned in the system are registered and active.
The Canvas App is configured and accessible to the user.
<h4>Actions by Agent:</h4>

Create a new Contact with necessary information.
Create a new Case with necessary information and link it to the Contact created above.
Create a new Activity related to the Case or Contact.
Attach photos to the Contact, Case, and Activity records.
Set the value of "Is Confidential" field to either True or False.
<h4>Actions by Agent:</h4>

Assign the Case to the Customer Service Manager.
<h4>Actions by Manager:</h4>

Open the Case and resolve it by clicking on the "Resolve Case" button.
If the Manager needs some more information before resolving the case, he can create a Follow Up Task via Timeline or the Related Activities section.
In the Quick Create form of the Task, mark it as a Follow Up and select the Contact Lookup field.
On the basis of the "Is Confidential" field, the task will be assigned to either the "Escalation team" or the "Confidential Case Team" of the respected Business Unit.
<h4>Actions by Team Member:</h4>

Open the Follow Up Task view and check for the newly created Follow Up Task.
Navigate to the Follow Up Task Form and check the information provided by the Manager in the Task.
<h4>Post-conditions:</h4>

All the records created and updated during the test are saved successfully.
The Follow Up Task is assigned to the correct team and the team members can access the task.
The Canvas App is showing all the documents at one place on the Follow Up Task Form.
Only Manager is able to see Resolve Case Button.
