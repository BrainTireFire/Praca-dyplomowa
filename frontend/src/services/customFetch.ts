import { BASE_URL } from "./constAPI";

async function handleRefreshToken() {
  const refreshResponse = await fetch(`${BASE_URL}/api/token/refresh-token`, {
    method: "POST",
    credentials: "include",
  });

  if (refreshResponse.ok) {
    return true;
  }

  window.location.replace("/login");
  return false;
}

export async function customFetch(
  url: string,
  options: RequestInit = {}
): Promise<any> {
  try {
    const response = await fetch(url, {
      ...options,
      credentials: "include", // Ensure cookies are included
    });

    if (!response.ok) {
      switch (response.status) {
        case 401:
          const isTokenRefreshed = await handleRefreshToken();

          if (isTokenRefreshed) {
            const retryResponse = await fetch(url, {
              ...options,
              credentials: "include",
            });
            if (!retryResponse.ok) {
              // Handle non-OK responses from retry
              const errorText = await retryResponse.text();
              console.error(
                "Fetch error:",
                errorText ||
                  retryResponse.statusText ||
                  "An unexpected error occurred."
              );
              throw new Error("Unauthorized");
            }
            return retryResponse.json();
          }
          break;

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
