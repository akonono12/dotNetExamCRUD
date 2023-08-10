import { Contact } from "./contact";

export class SearchContact {
  public totalCount:number = 0;
  public pageIndex:number = 0;
  public pageSize:number = 0;
  public results:Contact[] = [];
}
