export interface PostCommentDto {
    postId:string;
    postCommentId:string;
    userProfileId:string;
    userFullName:string;
    text:string;
    createdAt:Date;
}