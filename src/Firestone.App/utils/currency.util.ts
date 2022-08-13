export const stringOrNumberAsCurrency = (value: string | number): string => {
  return value.toLocaleString("en-AU", { style: "currency", currency: "AUD" });
};

export const numberAsCurrency = (value?: number): string => {
  if (!value) return "-";

  return new Intl.NumberFormat("en-AU", {
    style: "currency",
    currency: "AUD",
  }).format(value);
};
