export function IsNullOrWhiteSpace(str: string | null | undefined): boolean {
  if (str == null || typeof str === 'undefined') return true;
  return str.trim().length == 0 || str.length == 0;
}
