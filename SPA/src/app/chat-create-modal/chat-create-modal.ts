import { Component, inject, signal, TemplateRef, WritableSignal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgbModal, NgbSlide } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { Participant } from '../_models/participant';

@Component({
  selector: 'app-chat-create-modal',
  imports: [ReactiveFormsModule, NgSelectModule],
  templateUrl: './chat-create-modal.html',
  styleUrl: './chat-create-modal.css',
})
export class ChatCreateModal {
  private modalService = inject(NgbModal);
  close: WritableSignal<string> = signal('');
  chatForm: FormGroup = new FormGroup({});
  users = [{id: 1, name: "Hola"},{id: 2, name: "cabeza"}];
  selectedUsers: any[] = [];

  constructor(private fb: FormBuilder) {}
  
  ngOnInit() {
    console.log("init")
    this.initializeForm();
  }

  initializeForm() {
    this.chatForm = this.fb.group({
      name: ["", Validators.required],
      participants: [[], Validators.required]
    })
  }

  registerChat() {
    console.log(this.chatForm.value);
  }

  open(content: TemplateRef<any>) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result;
  }

  lookFor(val: any) {
    return this.users.filter(usr => usr.name.includes(val));
  }

  addParticipant(participant: Participant) {
    this.selectedUsers.push(participant.id);
    console.log(this.selectedUsers);
  }
}
