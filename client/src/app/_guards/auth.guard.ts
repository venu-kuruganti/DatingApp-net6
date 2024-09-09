import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

export const authGuard: CanActivateFn = (route, state) => {
  const  accService  = inject(AccountService);
  const toastr = inject(ToastrService);

  if (accService.currentUser()) {
    return true;
  }
  else{
    toastr.error('You shall not PASS!');
    return false;
  }
};
