namespace AstroBhaskar.API.Constants
{
    public static class StaticValues
    {
        #region Routes
        public const string ApiRoutePrefix = "api/v1/";
        public const string UserPath = "user";
        public const string UserGetByIdPath = "user/get";
        public const string UserSearchPath = "user/search";
        public const string UserResetPasswordPath = "user/reset-password";
        public const string UserToggleBlockPath = "user/toggle-block";
        public const string UserToggleLockPath = "user/toggle-lock";
        public const string UserChangePasswordPath = "user/change-password";
        public const string UserPermissionPath = "user-permission";
        public const string UserPermissionGetByIdPath = "user-permission/get";
        public const string UserPermissionSearchPath = "user-permission/search";
        public const string MasterCollectionPath = "master-collection";
        public const string MasterCollectionDeleteByCollectionPath = "master-collection/delete-by-collection/{collectionName}";
        public const string MasterCollectionDeleteByCollectionKeyPath = "master-collection/delete-by-collection-key/{collectionName}/{key}";
        public const string MasterCollectionDeleteByIdPath = "master-collection/delete-by-id/{collectionId}";
        public const string MasterCollectionGetByIdPath = "master-collection/get-by-id/{collectionId}";
        public const string MasterCollectionGetByCollectionPath = "master-collection/get-by-collection/{collectionName}/{pageNo}/{pageSize}";
        public const string MasterCollectionSearchPath = "master-collection/search/{searchTerm}/{pageNo}/{pageSize}";
        #endregion

        #region Message
        public const string UserRequired = "UserIsRequired";
        public const string UserRequiredMessage = "User is required to preceed!";
        public const string UserPermissionRequired = "UserPermissionIsRequired";
        public const string UserPermissionRequiredMessage = "User permission is required to preceed!";
        public const string UserAlreadyExist = "UserAlreadyExist";
        public const string UserAlreadyExistMessage = "User is already exist!";
        public const string MasterCollectionAlreadyExist = "MasterCollectionAlreadyExist";
        public const string MasterCollectionAlreadyExistMessage = "Master collection is already exist with same collection name and key!";
        public const string MasterCollectionNotExist = "MasterCollectionDoesNotExist";
        public const string MasterCollectionNotExistMessage = "Master collection does not exist!";
        public const string UserNotFound = "InvalidUser";
        public const string UserNotFoundMessage = "User not found!";
        public const string UserPermissionNotFound = "InvalidPermission";
        public const string UserPermissionNotFoundMessage = "User permission not found!";
        public const string InvaliEmail = "Email is invalid!";
        #endregion

        #region Property Validation Message
        public const string RequiredMessage = " is required!";
        #endregion

    }
}
