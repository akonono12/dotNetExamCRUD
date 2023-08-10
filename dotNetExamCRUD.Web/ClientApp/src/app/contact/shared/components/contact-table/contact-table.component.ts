import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Contact } from '../../model/contact';
import { SearchContact } from '../../model/search-contact';
import { ContactService } from '../../contact.service';

@Component({
  selector: 'app-contact-table',
  templateUrl: './contact-table.component.html',
  styleUrls: ['./contact-table.component.css']
})
export class ContactTableComponent implements OnInit {

  public contactFilter: Contact = new Contact();
  public contacts:Contact[] = [];
  public pageIndex:number = 1;
  public totalCount:number = 0;
  public searchList:SearchContact = new SearchContact();
  @Output() edit: EventEmitter<Contact> = new EventEmitter();
  constructor(private contactService:ContactService) { }

  ngOnInit() {
  }

  public loadTable(){
    this.contactService.searchContact(this.pageIndex,this.contactFilter).subscribe(data => {
      this.contacts = data.results;
      this.totalCount = data.totalCount;
      this.pageIndex = data.pageIndex;
      this.searchList = data;
    })
  }

  public handlePageChange(event: number): void {
    this.pageIndex = event;
    this.loadTable();
  }

  public deleteContact(model:Contact){
    if(confirm(`Are you sure you want to delete: ${model.firstName} ${model.lastName} ?`)){
      this.contactService.deleteContact(model.contactId).subscribe(result => {
        if(result.success){
          alert("Succesfully deleted");
          this.loadTable();
        }else{
          result.errors.forEach(x => {
            alert(x);
          })
        }
      },() => alert("Something wrong while deleting.Please contact the administrator."))
    }
  }

  public onClickEdit(model:Contact){
    this.edit.emit(model);
  }

  keyPress(event: any) {
    const pattern = /[0-9\+\-\ ]/;

    let inputChar = String.fromCharCode(event.charCode);
    if (event.keyCode != 8 && !pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  public search(){
    this.pageIndex = 1;
    this.loadTable();
  }

}
