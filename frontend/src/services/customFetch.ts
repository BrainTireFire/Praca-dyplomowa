export async function customFetch(
  url: string,
  options: RequestInit = {}
): Promise<any> {
  try {
    const response = await fetch(url, {
      ...options,
      credentials: "include", // Ensure cookies are included
    });

    switch (response.status) {
      case 401:
        window.location.replace("/login");
        throw new Error("Unauthorized");
      case 403:
        window.location.replace("/forbidden");
        throw new Error("Forbidden");
      case 404:
        window.location.replace("/notFound");
        throw new Error("NotFound");
      case 500:
        window.location.replace("/serviceDown");
        throw new Error("ServerError");
      default:
        break;
    }

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(
        errorText || response.statusText || "An unexpected error occurred."
      );
    }

    const contentType = response.headers.get("Content-Type");
    if (contentType && contentType.includes("application/json")) {
      return response.json();
    } else {
      return {};
    }
  } catch (error) {
    if (error instanceof TypeError && error.message === "Failed to fetch") {
      console.error("Fetch error: Network error, server may be down");
      window.location.replace("/serviceDown");
      throw new Error("FailedToFetch");
    } else {
      console.error("Fetch error:", error);
      throw error;
    }
  }
}
