import { Component, Inject, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { Member } from '../../_models/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {

  private membersService = inject(MembersService);
   members: Member[];

  ngOnInit(): void {
    this.loadMembers();
    console.log(this.members);
  }

  loadMembers(){
    this.membersService.getMembers().subscribe({
      next: _members => this.members = _members
    });
  }

}
