import { Component, Input, OnInit } from '@angular/core';
import { Contact } from '../../model/contact';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {

  public GUIDEMPTY:string = '00000000-0000-0000-0000-000000000000';
  @Input() public contact:Contact = new Contact();
  constructor() { }

  ngOnInit() {
  }


  keyPress(event: any) {
    const pattern = /[0-9\+\-\ ]/;

    let inputChar = String.fromCharCode(event.charCode);
    if (event.keyCode != 8 && !pattern.test(inputChar)) {
      event.preventDefault();
    }
  }
}
