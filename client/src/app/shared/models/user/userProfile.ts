import { BasicInfo } from "./basicInfo";

export interface UserProfile{
    userProfileId:string;
    identityId:string;
    createdAt:Date;
    lastModified:Date;
    role:string;
    basicInfo: BasicInfo;
}