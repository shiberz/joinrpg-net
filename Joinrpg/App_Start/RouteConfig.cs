﻿using System.Web.Mvc;
using System.Web.Routing;

namespace JoinRpg.Web
{
  public static class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.LowercaseUrls = true;
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(name: "ProjectHome", url: "{ProjectId}/home",
        defaults: new {controller = "Game", action = "Details"});

      routes.MapRoute(name: "ProjectEdit", url: "{ProjectId}/edit",
        defaults: new {controller = "Game", action = "Edit"});

      routes.MapRoute(name: "ProjectTools", url: "{ProjectId}/tools/{action}",
        defaults: new { controller = "GameTools", action = "Index" });

      routes.MapRoute(name: "MyClaims", url: "my/claims", defaults: new {controller = "Claim", action = "my"});

      routes.MapRoute(name: "GroupAddClaim", url: "{ProjectId}/roles/{CharacterGroupId}/apply",
        defaults: new {controller = "Claim", action = "AddForGroup"});

      routes.MapRoute(name: "GroupListClaim", url: "{ProjectId}/roles/{CharacterGroupId}/claims",
        defaults: new {controller = "Claim", action = "ListForGroup"});

      routes.MapRoute(name: "AddCharacter", url: "{ProjectId}/roles/{CharacterGroupId}/add-char",
        defaults: new {controller = "Character", action = "Create",});

      routes.MapRoute(name: "ProjectRoles", url: "{ProjectId}/roles/{CharacterGroupId}",
        defaults: new {controller = "GameGroups", action = "Index", CharacterGroupId = UrlParameter.Optional});

      routes.MapRoute(name: "ProjectRolesAction", url: "{ProjectId}/roles/{CharacterGroupId}/{action}",
        defaults: new {controller = "GameGroups", action = "Index",});

      routes.MapRoute(name: "ProjectAclAdd", url: "acl/add",
        defaults: new { controller = "Acl", action = "Add"});

      routes.MapRoute(name: "ProjectAcls", url: "{ProjectId}/acl/{ProjectAclId}/{action}",
        defaults: new {controller = "Acl", action = "Index", ProjectAclId = UrlParameter.Optional});

      routes.MapRoute(name: "ProjectFieldsCreate", url: "{ProjectId}/fields/create",
        defaults: new
        {
          controller = "GameField",
          action = "Create"
        });

      routes.MapRoute(name: "ProjectFields", url: "{ProjectId}/fields/{ProjectCharacterFieldId}/{action}",
        defaults: new {controller = "GameField", action = "Index", ProjectCharacterFieldId = UrlParameter.Optional});

      routes.MapRoute(name: "CharacterAddClaim", url: "{ProjectId}/character/{CharacterId}/apply",
        defaults: new {controller = "Claim", action = "AddForCharacter"});

      routes.MapRoute(name: "ClaimResp", url: "{ProjectId}/claims/for-master/{ResponsibleMasterId}",
        defaults: new {controller = "Claim", action = "Responsible"});

      routes.MapRoute(name: "ClaimRespMaster", url: "{ProjectId}/claims/problems-for-master/{ResponsibleMasterId}",
        defaults: new { controller = "Claim", action = "ResponsibleProblems" });

      routes.MapRoute(name: "Claim", url: "{ProjectId}/claim/{ClaimId}/{action}",
        defaults: new { controller = "Claim", action = "Edit" });

      routes.MapRoute(name: "MyClaim", url: "{ProjectId}/myclaim",
        defaults: new { controller = "Claim", action = "MyClaim" });

      routes.MapRoute(name: "ClaimActions", url: "{ProjectId}/claims/{action}",
        defaults: new { controller = "Claim", action = "Index" });

      routes.MapRoute(name: "PlotWithId", url: "{ProjectId}/plot/{PlotFolderId}/{action}",
        defaults: new {controller = "Plot", action = "Index"});

      routes.MapRoute(name: "Plot", url: "{ProjectId}/plots/{action}",
        defaults: new {controller = "Plot", action = "Index"});

      routes.MapRoute(name: "Character", url: "{ProjectId}/character/{CharacterId}/{action}",
        defaults: new {controller = "Character", action = "Details"});


      routes.MapRoute(name: "Finances", url: "{ProjectId}/money/{action}",
        defaults: new { controller = "Finances", action = "Setup" });

      routes.MapRoute(name: "UserMe", url: "user/me",
        defaults: new { controller = "User", action = "Me"});

      routes.MapRoute(name: "User", url: "user/{UserId}/{action}",
        defaults: new {controller = "User", action = "Details"});

      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
        );

    }
  }
}