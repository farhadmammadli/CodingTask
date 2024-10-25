import Swal from 'sweetalert2';
import { AxiosError } from 'axios';

export function handleError(error: unknown): void {
  const message = ((error as AxiosError)?.response?.data as { message?: string })?.message 
                || 'An error occurred.';
  Swal.fire({
    icon: 'error',
    title: 'Error',
    text: message
  });
}
