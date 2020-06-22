import { createHash } from "https://deno.land/std/hash/mod.ts";

type Props<T> = Pick<T, Exclude<({ [K in keyof T]: T[K] extends Function ? never : K; }[keyof T]), undefined>>;

function assert(expression: any): asserts expression {
  if (!expression) { throw new Error("invalid expression"); }
}

class Transaction {
  #secret = new Uint8Array();
  amount = 0;
  receivedHash = "";

  constructor(init?: { amount?: number; expectedHash?: string; }) {
    this.amount = init?.amount ?? 0;
    this.receivedHash = init?.expectedHash ?? "";
  }

  validateAmount(receivedAmount: number) {
    if (this.amount !== receivedAmount) throw new Error("amount validation failed");
  }

  validateHashFromSecret(expectedHash: string) {
    const actualHash = createHash("sha256").update(this.#secret).toString();
    if (actualHash !== expectedHash) {
      throw new Error("hash validation failed");
    }
  }

  validateSecretFromHash(receivedSecret: Uint8Array) {
    const actualHash = createHash("sha256").update(receivedSecret).toString();
    if (actualHash !== this.receivedHash) {
      throw new Error("secret validation failed");
    }
  }

  createSecret() {
    this.#secret = new Uint8Array([1, 2, 3]);
  }

  setSecret(secret: Uint8Array) {
    this.#secret = secret;
  }

  getSecret() {
    return this.#secret;
  }

  getHashFromSecret() {
    return createHash("sha256").update(this.#secret).toString();
  }
}

interface Person {
  name: string;
  transaction?: Transaction;
}

// alice wants to send 5 btc to dave
// alice -> bob -> dave

const alice: Person = { name: "alice" };
const bob: Person = { name: "bob" };
const carol: Person = { name: "carol" };
const dave: Person = { name: "dave" };

sendHtlcs();
revealSecret();

function sendHtlcs() {
  // alice tells dave she want to pay him 5 btc
  // messages sent...

  // dave creates secret and hash
  dave.transaction = new Transaction({ amount: 5 });
  dave.transaction.createSecret();

  // dave sends transaction w/ hash to alice
  alice.transaction = new Transaction({ amount: dave.transaction.amount, expectedHash: dave.transaction.getHashFromSecret() });

  // alice sends transaction w/ hash to bob
  bob.transaction = new Transaction({ amount: alice.transaction.amount, expectedHash: alice.transaction.receivedHash });

  // bob sends transaction w/ hash to carol
  carol.transaction = new Transaction({ amount: bob.transaction.amount, expectedHash: bob.transaction.receivedHash });

  // carol sends transaction w/ hash to dave
  dave.transaction.validateAmount(carol.transaction.amount);
  dave.transaction.validateHashFromSecret(carol.transaction.receivedHash);
}

function revealSecret() {
  // dave reveals secret to carol
  carol.transaction?.validateSecretFromHash(dave.transaction!.getSecret());
  carol.transaction?.setSecret(dave.transaction?.getSecret()!);

  // carol reveals secret to bob
  bob.transaction?.validateSecretFromHash(carol.transaction!.getSecret());
  bob.transaction?.setSecret(carol.transaction?.getSecret()!);
}
