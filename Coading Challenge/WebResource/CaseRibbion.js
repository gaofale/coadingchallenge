"use strict";

if (typeof (TFL) === "undefined") var TFL = {};
if (typeof (TFL.Case) === "undefined") TFL.Case = {};

TFL.Case.Ribbon = {
    ResolveCaseEnableRule: function (roleName) {
        let hasRole = false;
        let roles = Xrm.Utility.getGlobalContext().userSettings.roles;
        roles.forEach(x => {
            if (x.name.toLowerCase() === roleName.toLowerCase()) {
                hasRole = true;
                return;
            }
        });
        return hasRole;
    }
}