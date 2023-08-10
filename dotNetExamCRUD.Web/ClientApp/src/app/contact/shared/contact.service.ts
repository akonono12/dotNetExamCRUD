import { CommandResult } from './model/commandResult';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Contact } from './model/contact';
import { SearchContact } from './model/search-contact';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private contactUrl:string = environment.baseUrl;
  constructor(private httpClient: HttpClient) { }

  public addContact(contact:Contact){
    return this.httpClient.post<CommandResult<string>>(`${this.contactUrl}/`,{...contact})
  }

  public updateContact(contact:Contact){
    return this.httpClient.put<CommandResult<boolean>>(`${this.contactUrl}/`,{...contact})
  }

  public deleteContact(id:string){
    return this.httpClient.delete<CommandResult<boolean>>(`${this.contactUrl}/`,{body:{id}})
  }

  public searchContact(pageIndex:number,filter:Contact){
    return this.httpClient.get<SearchContact>(`${this.contactUrl}/search?pageIndex=${pageIndex}&filter.lastName=${filter.lastName}
    &filter.firstName=${filter.firstName}&filter.companyName=${filter.companyName}&filter.email=${filter.email}&filter.contactNumber=${filter.contactNumber}`)
  }



}
