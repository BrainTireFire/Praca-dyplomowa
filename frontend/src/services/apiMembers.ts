export async function getMembers(): Promise<Member[]> {
  const response = await fetch("http://localhost:3000/members");

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const data: Member[] = await response.json();

  return data;
}

export async function getMember(memberId: number): Promise<Member> {
  const response = await fetch(`http://localhost:3000/members/${memberId}`);

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  const member: Member = await response.json();
  return member;
}
