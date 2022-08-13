const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

export const dropDate = (date?: Date): string => {
  if (!date) return "Unknown";

  const d = new Date(date);

  const year = d.getFullYear();
  const month = d.getMonth();

  return `${months[month]} ${year}`;
};
