export interface ModCombineRequest {
  name: string;
  description: string;
  isPrivate: boolean;
  categoryId: number;
  newUrls?: string[];
}

export interface ModUpdateCombineRequest extends ModCombineRequest {
  updatedUrls?: UrlUpdateRequest[];
  urlIdsToDelete?: number[];
}

export interface UrlUpdateRequest {
  id: number;
  url: string;
}

export class Mod {
  id: number = 0;
  title: string = ""
  authorId: number = 0
  authorDisplayName: string = ""
  authorAvatarUrl: string = ""
  createdAt: string = ""
  isPrivate: boolean = false
  updatedAt: string = ""
  category: string = ""
}
export class ModWithImage extends Mod {
  image: string = ""
}
export class ModDetail extends Mod {
  content: string = ""
  categoryId: number = 0
  urls: Url[] = []
}

export class ModDetailInternal {
  id: number = 0
  title: string = ""
  description: string = ""
  isPrivate: boolean = false
  categoryId: number = 0
  urls: Url[] = []
}

export class Url {
  id: number = 0
  url: string = ""
}

export class React {
  id: number = 0
  content: string = ''
  authorId: number = 0
  authorDisplayName: string = ""
  authorAvatarUrl: string = ""
  createdAt: string = ""
  updateAt: string = ""
}
