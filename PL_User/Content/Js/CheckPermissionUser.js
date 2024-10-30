$(document).ready(function () {
    $.ajax({
        url: "/User/GetPermissionUser",
        type: "GET",
        success: function (response) {
            if (response.success && response.permission) {
                var listPermission = JSON.stringify(response.permission)
                window.localStorage.setItem("permissions", listPermission);
                console.log("fecth success");
            } else {
                console.log("Failed to fetch permissions");
            }
        },
        error: function (xhr) {
            console.log(xhr);
        }
    });
})
function CheckPermission(role) {
    var permissions = window.localStorage.getItem('permissions');
    if (permissions) {
        var permissionData = JSON.parse(permissions);
        var isPermission = permissionData.find(u => u === role);
        if (isPermission) return true;
        return false;
    }
    return false;
}