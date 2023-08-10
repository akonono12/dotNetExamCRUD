import { Component, OnInit, ViewChild } from '@angular/core';
import { ContactService } from './shared/contact.service';
import { ContactTableComponent } from './shared/components/contact-table/contact-table.component';
import { Contact } from './shared/model/contact';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  public contact:Contact = new Contact();
  public GUIDEMPTY:string = '00000000-0000-0000-0000-000000000000';
  @ViewChild("table",{static:false}) contactTable!:ContactTableComponent;
  constructor(private contactService:ContactService) { }
  ngAfterViewInit(): void {
    this.contactTable.loadTable();
  }

  ngOnInit() {

  }

  public mappedModel(model:Contact){
    this.contact = model;
  }

  private addContact(){
    this.contactService.addContact(this.contact).subscribe(data => {
      if(data.success){
        alert("Successfully added");
        this.contactTable.loadTable();
      }else{
        data.errors.forEach(x => {
          alert(x);
        })
      }
    },() => alert("Something wrong while adding.Please contact the administrator."))
  }

  private updateContact(){
    this.contactService.updateContact(this.contact).subscribe(data =>{
      if(data.success){
        alert("Succesfully updated");
        this.contactTable.loadTable();
      }else{
        this.contactTable.loadTable();
        data.errors.forEach(x => {
          alert(x);
        })
      }
    },() => alert("Something wrong while updating.Please contact the administrator."))
  }

  public save(){
    if(this.contact.contactId != this.GUIDEMPTY){
      this.updateContact();
    }else{
      this.addContact();
    }
    this.contact = new Contact();
  }
}
