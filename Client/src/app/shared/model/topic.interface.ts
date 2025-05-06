export class Topic {
  id: number = 0;
  title: string = '';
  content: string = '';
  authorId: number = 0;
  createdAt: string = '';
  updatedAt: string = '';
  commentCount: number = 0;
  authorAvatarUrl: string = '';
  authorDisplayName: string = '';
}

export class TopicDetail extends Topic {
}

export class Reply {
  id: string = '';
  content: string = '';
  authorId: string = '';
  authorDisplayName: string = '';
  authorAvatarUrl: string = '';
  createdAt: string = '';
  parentCommentId?: string = '';
}
